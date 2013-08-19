namespace libeveapi
{
    /// <summary>
    /// Represents the seven wallet divisions available to corporations.
    /// Casting from int to WalletDivision is possible because WalletDivision is numbered 1000 till 1006.
    /// </summary>
    public enum WalletDivision
    {
        /// <summary>
        /// Master Wallet
        /// </summary>
        Master = 1000,
        /// <summary>
        /// Wallet two
        /// </summary>
        Two = 1001,
        /// <summary>
        /// Wallet three
        /// </summary>
        Three = 1002,
        /// <summary>
        /// Wallet four
        /// </summary>
        Four = 1003,
        /// <summary>
        /// Wallet Five
        /// </summary>
        Five = 1004,
        /// <summary>
        /// Wallet six
        /// </summary>
        Six = 1005,
        /// <summary>
        /// Wallet seven
        /// </summary>
        Seven = 1006
    }
}