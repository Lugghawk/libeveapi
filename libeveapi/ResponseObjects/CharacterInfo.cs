namespace libeveapi
{

    ///<summary>
    ///Character Information Sheet
    ///</summary>
    public class CharacterInfo : ApiResponse
    {
        /// <summary>
        /// API Version compatability
        /// </summary>
        public const string API_VERSION = "2";
        /// <summary>
        /// ID of character
        /// </summary>
        public int characterId{get;set;}
        /// <summary>
        /// Character name
        /// </summary>
        public string name{get;set;}
        /// <summary>
        /// Character race
        /// </summary>
        public string race{get;set;}
        /// <summary>
        /// Character Bloodline
        /// </summary>
        public string bloodLine{get;set;}
        /// <summary>
        /// Character Corporation Name
        /// </summary>
        public string corporationName{get;set;}
        /// <summary>
        /// Character wallet balance 
        /// </summary>
        public double balance{get;set;}
        /// <summary>
        /// Character Alliance Name
        /// </summary>
        public string alliance{get;set;}
        /// <summary>
        /// Character last known location
        /// </summary>
        public string location{get;set;}
        /// <summary>
        /// Character security status
        /// </summary>
        public double secStatus{get;set;}
        /// <summary>
        /// Character's last known shiptype.
        /// </summary>
        public string shipType{get;set;}
        /// <summary>
        /// Character's last known ship's name.
        /// </summary>
        public string shipName{get;set;}
        
        
        
    }
}