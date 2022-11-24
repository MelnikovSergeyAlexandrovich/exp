namespace console_explorer
{
    public class Fso
    {
        public string Name { get; private set; }
        public long Size { get; private set; }
        public FsoType FsoType { get; private set; }
        public string FullFilename { get; private set; }

        public Fso(string name, FsoType fsoType, long size, string fullFilename)
        {
            Name = name;
            Size = size;
            FsoType = fsoType;
            FullFilename = fullFilename;
        }
    }
}