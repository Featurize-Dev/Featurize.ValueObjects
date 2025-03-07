namespace Featurize.ValueObjects.Metric;
public static class ImperialSystem
{
    public static Unit Fathom => new(1, "Fathom", "ftm", 6.0761, Foot);
    public static Unit Cable => new(1, "Cable", "cb", 607.61, Foot);
    public static Unit NauticalMile => new(1, "Nautical Mile", "nmi", 6076.1, Foot);
        
    public static Unit Link => new(1, "Link", "li", 1/100, Chain);
    public static Unit Rod => new(1, "Rod", "rd", 66 / 4, Foot);


    public static Unit League => new(1, "League", "lea", 15840, Foot);
    public static Unit Mile => new(1, "Mile", "mi", 5280, Foot);
    public static Unit Furlong => new(1, "Furlong", "fur", 660, Foot);
    public static Unit Chain => new(1, "Chain", "ch", 66, Foot);
    public static Unit Yard => new(1, "Yard", "yd", 3, Foot);
    public static Unit Foot => new(1, "Foot", "ft", 1);
    public static Unit Hand => new(1, "Hand", "hd", 1/3, Foot);
    public static Unit Inch => new(1, "Inch", "in", 1/12, Foot);
    
    public static Unit Barleycorn => new(1, "Barleycorn", "bc", 1/36, Foot);
    public static Unit Thou => new(1, "Thou", "th", 1/12000, Foot);
    public static Unit Twip => new(1, "Twip", "tw", 1/17280, Foot);
}
