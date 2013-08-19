using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class AssetListTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetAssetList()
        {
            AssetList assetList = EveApi.GetAssetList(AssetListType.Corporation, 432435, 234523, "apiKey");

            AssetList.AssetListItem parent = GetAsset(197977287, assetList.AssetListItems);
            AssetList.AssetListItem child = GetAsset(331125392, parent.Contents);
            AssetList.AssetListItem grandChild = GetAsset(109617951, child.Contents);

            Assert.AreEqual(197977287, parent.ItemId);
            Assert.AreEqual(331125392, child.ItemId);
            Assert.AreEqual(109617951, grandChild.ItemId);
        }

        [Test]
        public void PersistAssetList()
        {
            ResponseCache.Clear();
            AssetList assetList = EveApi.GetAssetList(AssetListType.Corporation, 432435, 234523, "apiKey");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            AssetList cachedAssetList = EveApi.GetAssetList(AssetListType.Corporation, 432435, 234523, "apiKey");

            Assert.AreEqual(assetList.CachedUntilLocal, cachedAssetList.CachedUntilLocal);

            for (int i = 0; i < assetList.AssetListItems.Length; i++)
            {
                Assert.AreEqual(true, CompareAssets(assetList.AssetListItems[i], cachedAssetList.AssetListItems[i]));
            }
        }

        private AssetList.AssetListItem GetAsset(int itemId, AssetList.AssetListItem[] assets)
        {
            foreach (AssetList.AssetListItem asset in assets)
            {
                if (asset.ItemId == itemId)
                {
                    return asset;
                }
            }

            return null;
        }

        private bool CompareAssets(AssetList.AssetListItem item1, AssetList.AssetListItem item2)
        {
            if (item1.ItemId != item2.ItemId)
            {
                return false;
            }

            for (int i = 0; i < item1.Contents.Length; i++)
            {
                if (CompareAssets(item1.Contents[i], item2.Contents[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
