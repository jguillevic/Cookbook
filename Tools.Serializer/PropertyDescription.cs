namespace Tools.Serializer
{
    public class PropertyDescription
    {
        public string Name { get; set; }
        public string ShortName { get; set; }

        public string GetName(bool useShortName)
        {
            if (useShortName)
                return ShortName;
            else
                return Name;
        }
    }
}
