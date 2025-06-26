namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Mood.Set
{
    public class MoodCacheDto
    {
        public Dictionary<Guid, Dictionary<string, int>> MoodsByLocationId { get; set; }

        public Dictionary<string, int> AddMoodVote(int quantityOfMoodSets, string mood, Dictionary<string, int> modsByAddressId)
        {
            if (quantityOfMoodSets == 0)
                modsByAddressId[mood] = 1;

            modsByAddressId[mood] = quantityOfMoodSets + 1;

            return modsByAddressId;
        }
    }
}
