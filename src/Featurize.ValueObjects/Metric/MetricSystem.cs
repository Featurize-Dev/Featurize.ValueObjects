using System.Diagnostics.Metrics;

namespace Featurize.ValueObjects.Metric;

public static class MetricSystem
{
    public static class Mass
    {
        /// <summary>
        /// 
        /// </summary>
        public static Unit[] All = { 
            Yottagram, Zettagram, Exagram, 
            Petagram, Teragram, Gigagram, 
            Megagram, Kilogram, Hectogram, 
            Decagram, Gram, Decigram, 
            Centigram, Milligram, Microgram, 
            Nanogram, Picogram, Femtogram, 
            Attogram, Zeptogram 
        };

        public static Unit Yottagram => new(1, nameof(Yottagram), "Yg", 1e24, Gram);
        public static Unit Zettagram => new(1, nameof(Zettagram), "Zg", 1e21, Gram);
        public static Unit Exagram => new(1, nameof(Exagram), "Eg", 1e18, Gram);
        public static Unit Petagram => new(1, nameof(Petagram), "Pg", 1e15, Gram);
        public static Unit Teragram => new(1, nameof(Teragram), "Tg", 1e12, Gram);
        public static Unit Gigagram => new(1, nameof(Gigagram), "Gg", 1e9, Gram);
        public static Unit Megagram => new(1, nameof(Megagram), "Mg", 1e6, Gram);
        public static Unit Kilogram => new (1, nameof(Kilogram), "kg", 1e3, Gram);
        public static Unit Hectogram => new(1, nameof(Hectogram), "hg", 1e2, Gram);
        public static Unit Decagram => new(1, nameof(Decagram), "dag", 10, Gram);
        public static Unit Gram => new(1, nameof(Gram), "g", 1);
        public static Unit Decigram => new(1, nameof(Decigram), "dg", 1e-1, Gram);
        public static Unit Centigram => new(1, nameof(Centigram), "cg", 1e-2, Gram);
        public static Unit Milligram => new(1, nameof(Milligram), "mg", 1e-3, Gram);
        public static Unit Microgram => new(1, nameof(Microgram), "μg", 1e-6, Gram);
        public static Unit Nanogram => new(1, nameof(Nanogram), "ng", 1e-9, Gram);
        public static Unit Picogram => new(1, nameof(Picogram), "pg", 1e-12, Gram);
        public static Unit Femtogram => new(1, nameof(Femtogram), "fg", 1e-15, Gram);
        public static Unit Attogram => new(1, nameof(Attogram), "ag", 1e-18, Gram);
        public static Unit Zeptogram => new(1, nameof(Zeptogram), "zg", 1e-21, Gram);


        public static Unit Megaton => new(1, "Megaton", "mt", 1e6, MetricTon);
        public static Unit MetricTon => new(1, "Metric ton", "t", 1e6, Gram);
    }

    public static class Length
    {
        public static Unit[] All = { 
            Yottametre, Zettametre, Exametre, 
            Petametre, Terametre, Gigametre, 
            Megametre, Kilometre, Hectometre, 
            Decametre, Metre, Decimetre, 
            Centimetre, Millimetre, Micrometre, 
            Nanometre, Picometre, Femtometre, 
            Attometre, Zeptometre, Yoctometre 
        };

        public static Unit Yottametre => new(1, "Yottametre", "Ym", 1e24, Metre);
        public static Unit Zettametre => new(1, "Zettametre", "Zm", 1e21, Metre);
        public static Unit Exametre => new(1, "Exametre", "Em", 1e18, Metre);
        public static Unit Petametre => new(1, "Petametre", "Pm", 1e15, Metre);
        public static Unit Terametre => new(1, "Terametre", "Tm", 1e12, Metre);
        public static Unit Gigametre => new(1, "Gigametre", "Gm", 1e9, Metre);
        public static Unit Megametre => new(1, "Megametre", "Mm", 1e6, Metre);
        public static Unit Kilometre => new(1, "Metre", "km", 1e3, Metre);
        public static Unit Decametre => new(1, "Decametre", "dam", 1e1, Metre);
        public static Unit Hectometre => new(1, "Hectometre", "hm", 1e2, Metre);
        public static Unit Metre => new(1, "Metre", "m", 1, null);
        public static Unit Decimetre => new(1, "Decimetre", "dm", 1e-1, Metre);
        public static Unit Centimetre => new(1, "Centimetre", "cm", 1e-2, Metre);
        public static Unit Millimetre => new(1, "Millimetre", "mm", 1e-3, Metre);
        public static Unit Micrometre => new(1, "Micrometre", "μm", 1e-6, Metre);
        public static Unit Nanometre => new(1, "Nanometre", "nm", 1e-9, Metre);
        public static Unit Picometre => new(1, "Picometre", "pm", 1e-12, Metre);
        public static Unit Femtometre => new(1, "Femtometre", "fm", 1e-15, Metre);
        public static Unit Attometre => new(1, "Attometre", "am", 1e-18, Metre);
        public static Unit Zeptometre => new(1, "Zeptometre", "zm", 1e-21, Metre);
        public static Unit Yoctometre => new(1, "Yoctometre", "ym", 1e-24, Metre);
    }

    public static class Area
    {
        public static Unit[] All = { 
            SquareYottametre, SquareZettametre, SquareExametre, 
            SquarePetametre, SquareTerametre, SquareGigametre, 
            SquareMegametre, SquareKilometre, SquareHectometre, 
            SquareDecametre, SquareMetre, SquareDecimetre, 
            SquareCentimetre, SquareMillimetre, SquareMicrometre, 
            SquareNanometre, SquarePicometre, SquareFemtometre, 
            SquareAttometre, SquareZeptometre, SquareYoctometre,

            YottaAre, ZettaAre, ExaAre,
            PetaAre, TeraAre, GigaAre,
            MegaAre, KiloAre, HectoAre,
            DecaAre, Are, DeciAre,
            CentiAre
        };

        public static Unit SquareYottametre => new(1, "Square Yottametre", "Ym²", 1e48, SquareMetre);
        public static Unit SquareZettametre => new(1, "Square Zettametre", "Zm²", 1e42, SquareMetre);
        public static Unit SquareExametre => new(1, "Square Exametre", "Em²", 1e36, SquareMetre);
        public static Unit SquarePetametre => new(1, "Square Petametre", "Pm²", 1e30, SquareMetre);
        public static Unit SquareTerametre => new(1, "Square Terametre", "Tm²", 1e24, SquareMetre);
        public static Unit SquareGigametre => new(1, "Square Gigametre", "Gm²", 1e18, SquareMetre);
        public static Unit SquareMegametre => new(1, "Square Megametre", "Mm²", 1e12, SquareMetre);
        public static Unit SquareKilometre => new(1, "Square Kilometre", "Km²", 1e6, SquareMetre);
        public static Unit SquareHectometre => new(1, "Square Hectometre", "Hm²", 1e4, SquareMetre);
        public static Unit SquareDecametre => new(1, "Square Decametre", "dam²", 1e2, SquareMetre);
        public static Unit SquareMetre => new(1, "Square Metre", "m²", 1);
        public static Unit SquareDecimetre => new(1, "Square Decimetre", "dm²", 1e-2, SquareMetre);
        public static Unit SquareCentimetre => new(1, "Square Centimetre", "cm²", 1e-4, SquareMetre);
        public static Unit SquareMillimetre => new(1, "Square Millimetre", "mm²", 1e-6, SquareMetre);
        public static Unit SquareMicrometre => new(1, "Square Micrometre", "μm²", 1e-12, SquareMetre);
        public static Unit SquareNanometre => new(1, "Square Nanometre", "nm²", 1e-18, SquareMetre);
        public static Unit SquarePicometre => new(1, "Square Picometre", "pm²", 1e-24, SquareMetre);
        public static Unit SquareFemtometre => new(1, "Square Femtometre", "fm²", 1e-30, SquareMetre);
        public static Unit SquareAttometre => new(1, "Square Attometre", "am²", 1e-36, SquareMetre);
        public static Unit SquareZeptometre => new(1, "Square Zeptometre", "zm²", 1e-42, SquareMetre);
        public static Unit SquareYoctometre => new(1, "Square Yoctometre", "ym²", 1e-48, SquareMetre);

        public static Unit YottaAre => new(1, "YottaAre", "Ya", 1e24, Are);
        public static Unit ZettaAre => new(1, "ZettaAre", "Za", 1e21, Are);
        public static Unit ExaAre => new(1, "ExaAre", "Ea", 1e18, Are);
        public static Unit PetaAre => new(1, "PetaAre", "Pa", 1e15, Are);
        public static Unit TeraAre => new(1, "TeraAre", "Ta", 1e12, Are);
        public static Unit GigaAre => new(1, "GigaAre", "Ga", 1e9, Are);
        public static Unit MegaAre => new(1, "MegaAre", "Ma", 1e6, Are);    
        public static Unit KiloAre => new(1, "KiloAre", "ka", 1e3, Are);
        public static Unit HectoAre => new(1, "HectoAre", "ha", 1e2, Are);
        public static Unit DecaAre => new(1, "DecaAre", "daa", 1e1, Are);
        public static Unit Are => new(1, "Are", "a", 100, SquareMetre);
        public static Unit DeciAre => new(1, "DeciAre", "da", 1e-1, Are);
        public static Unit CentiAre => new(1, "CentiAre", "ca", 1e-2, Are);
    }

    public static class Volume
    {
        public static Unit[] All = { 
            CubicYottametre, CubicZettametre, CubicExametre, 
            CubicPetametre, CubicTerametre, CubicGigametre, 
            CubicMegametre, CubicKilometre, CubicHectometre, 
            CubicDecametre, CubicMetre, CubicDecimetre, 
            CubicCentimetre, CubicMillimetre, CubicMicrometre, 
            CubicNanometre, CubicPicometre, CubicFemtometre, 
            CubicAttometre, CubicZeptometre, CubicYoctometre,

            Stere
        };

        public static Unit CubicYottametre => new(1, "Cubic Yottametre", "Ym³", 1e24, CubicMetre);
        public static Unit CubicZettametre => new(1, "Cubic Zettametre", "Zm³", 1e21, CubicMetre);
        public static Unit CubicExametre => new(1, "Cubic Exametre", "Em³", 1e18, CubicMetre);
        public static Unit CubicPetametre => new(1, "Cubic Petametre", "Pm³", 1e15, CubicMetre);
        public static Unit CubicTerametre => new(1, "Cubic Terametre", "Tm³", 1e12, CubicMetre);
        public static Unit CubicGigametre => new(1, "Cubic Gigametre", "Gm³", 1e9, CubicMetre);
        public static Unit CubicMegametre => new(1, "Cubic Megametre", "Mm³", 1e6, CubicMetre);
        public static Unit CubicKilometre => new(1, "Cubic Kilometre", "Km³", 1e3, CubicMetre);
        public static Unit CubicHectometre => new(1, "Cubic Hectometre", "Hm³", 1e2, CubicMetre);
        public static Unit CubicDecametre => new(1, "Cubic Decametre", "dam³", 10, CubicMetre);
        public static Unit CubicMetre => new(1, "Cubic Metre", "m³", 1);
        public static Unit CubicDecimetre => new(1, "Cubic Decimetre", "dm³", 1e-1, CubicMetre);
        public static Unit CubicCentimetre => new(1, "Cubic Centimetre", "cm³", 1e-3, CubicMetre);
        public static Unit CubicMillimetre => new(1, "Cubic Millimetre", "mm³", 1e-6, CubicMetre);
        public static Unit CubicMicrometre => new(1, "Cubic Micrometre", "μm³", 1e-9, CubicMetre);
        public static Unit CubicNanometre => new(1, "Cubic Nanometre", "nm³", 1e-9, CubicMetre);
        public static Unit CubicPicometre => new(1, "Cubic Picometre", "pm³", 1e-12, CubicMetre);
        public static Unit CubicFemtometre => new(1, "Cubic Femtometre", "fm³", 1e-15, CubicMetre);
        public static Unit CubicAttometre => new(1, "Cubic Attometre", "am³", 1e-18, CubicMetre);
        public static Unit CubicZeptometre => new(1, "Cubic Zeptometre", "zm³", 1e-21, CubicMetre);
        public static Unit CubicYoctometre => new(1, "Cubic Yoctometre", "ym³", 1e-24, CubicMetre);

        public static Unit Stere => new(1, "Stere", "s", 1, CubicMetre);
    }
    
    public static class Capacity
    {
        public static Unit[] All = { 
            YottaLitre, ZettaLitre, ExaLitre, 
            PetaLitre, TeraLitre, GigaLitre, 
            MegaLitre, KiloLitre, Hectolitre, 
            Litre, Decalitre, Decilitre, 
            Centilitre, Millilitre, Microlitre, 
            Nanolitre, Picolitre, Femtolitre, 
            Attolitre, Zeptolitre, Yoctolitre 
        };

        public static Unit YottaLitre => new(1, "YottaLitre", "Yl", 1e24, Litre);
        public static Unit ZettaLitre => new(1, "ZettaLitre", "Zl", 1e21, Litre);
        public static Unit ExaLitre => new(1, "ExaLitre", "El", 1e18, Litre);
        public static Unit PetaLitre => new(1, "PetaLitre", "Pl", 1e15, Litre);
        public static Unit TeraLitre => new(1, "TeraLitre", "Tl", 1e12, Litre);
        public static Unit GigaLitre => new(1, "GigaLitre", "Gl", 1e9, Litre);
        public static Unit MegaLitre => new(1, "MegaLitre", "Ml", 1e6, Litre);
        public static Unit KiloLitre => new(1, "KiloLitre", "kl", 1e3, Litre);
        
        public static Unit Hectolitre => new(1, "Hectolitre", "hl", 1e2, Litre);
        public static Unit Litre => new(1, "Litre", "l", 1);
        public static Unit Decalitre => new(1, "Decalitre", "dal", 1e1, Litre);
        public static Unit Decilitre => new(1, "Decilitre", "dl", 1e-1, Litre);
        public static Unit Centilitre => new(1, "Centilitre", "cl", 1e-2, Litre);
        public static Unit Millilitre => new(1, "Millilitre", "ml", 1e-3, Litre);
        public static Unit Microlitre => new(1, "Microlitre", "μl", 1e-6, Litre);
        public static Unit Nanolitre => new(1, "Nanolitre", "nl", 1e-9, Litre);
        public static Unit Picolitre => new(1, "Picolitre", "pl", 1e-12, Litre);
        public static Unit Femtolitre => new(1, "Femtolitre", "fl", 1e-15, Litre);
        public static Unit Attolitre => new(1, "Attolitre", "al", 1e-18, Litre);
        public static Unit Zeptolitre => new(1, "Zeptolitre", "zl", 1e-21, Litre);
        public static Unit Yoctolitre => new(1, "Yoctolitre", "yl", 1e-24, Litre);
    }

    public static class Temperature
    {
        public static Unit[] All = { 
            Kelvin, Celcius, Fahrenheit, Rankine 
        };

        public static Unit Rankine => new(1, "Rankine", "°R", 493.47, Kelvin);
        
        public static Unit Kelvin => new(1, "Kelvin", "K", 274.15, Celcius);

        public static Unit Fahrenheit => new(1, "Farenheit", "°F", 33.8, Celcius);

        public static Unit Celcius => new(1, "Celcius", "°C", 1);
    }
    
    public static bool TryParse(string s, IFormatProvider? provider, out Unit result)
    {
        result = Unit.Unknown;
        
        if (string.IsNullOrEmpty(s))
        {
            result = Unit.Empty;
            return true;
        }

        var parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
        {
            return false;
        }

        var value = double.Parse(parts[0]);
        var bases = Mass.All
            .Concat(Length.All)
            .Concat(Area.All)
            .Concat(Volume.All)
            .Concat(Capacity.All)
            .Concat(Temperature.All);
        var unit = bases.FirstOrDefault(x => x.Symbol.Equals(parts[1]));
        
        if(unit == null)
        {
            return false;
        }

        result = unit * value;

        return true;   
    }
}
