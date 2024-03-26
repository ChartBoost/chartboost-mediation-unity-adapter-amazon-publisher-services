namespace Chartboost.Mediation.AmazonPublisherServices
{
    #nullable enable
    /// <summary>
    /// Data struct to store all the Amazon pre bid settings from the configuration.
    /// </summary>
    public struct AmazonSettings
    {
        /// <summary>
        /// The Amazon placement name.
        /// </summary>
        public readonly string PartnerPlacement;

        /// <summary>
        /// The width of the expected ad if it's a banner.
        /// </summary>
        public readonly int? Width;

        /// <summary>
        /// The height of the expected ad if it's a banner.
        /// </summary>
        public readonly int? Height;

        /// <summary>
        /// Indicates if this is a video placement.
        /// </summary>
        public readonly bool? IsVideo;

        public AmazonSettings(string partnerPlacement, bool? isVideo, int? height, int? width)
        {
            PartnerPlacement = partnerPlacement;
            IsVideo = isVideo;
            Height = height;
            Width = width;
        }
    }
    #nullable disable
}
