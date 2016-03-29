namespace AccountBookSimu
{
    using StructIncreaseOptionPayPattern = StructRequiredPayPattern;

    public struct PayPattern
    {
        public StructRequiredPayPattern RequiredPayPattern;
        public StructTermOptionPayPattern TermOption;
        public StructIncreaseOptionPayPattern IncreaseOption;

        public PayPattern(int n)
        {
            RequiredPayPattern = new StructRequiredPayPattern(0);
            TermOption = new StructTermOptionPayPattern(0);
            IncreaseOption = new StructIncreaseOptionPayPattern(0);
        }
    }
}
