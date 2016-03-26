namespace AccountBookSimu
{
    using StructIncreaseOptionPayPattern = StructRequiredPayPattern;

    public struct PayPattern
    {
        public StructRequiredPayPattern RequiredPayPattern;
        public StructTermOptionPayPattern TermOption;
        public StructIncreaseOptionPayPattern IncreaseOption;
    }
}
