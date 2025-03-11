using System.Diagnostics.CodeAnalysis;

namespace Featurize.ValueObjects.Chemistry;

public static class PeriodicTable
{
    #region elements
    public static Element Hydrogen = new Element("H", "Hydrogen", 1);
    public static Element Helium = new Element("He", "Helium", 2);
    public static Element Lithium = new Element("Li", "Lithium", 3);
    public static Element Beryllium = new Element("Be", "Beryllium", 4);
    public static Element Boron = new Element("B", "Boron", 5);
    public static Element Carbon  = new Element("C", "Carbon", 6);
    public static Element Nitrogen = new Element("N", "Nitrogen", 7);
    public static Element Oxygen = new Element("O", "Oxygen", 8);
    public static Element Fluorine = new Element("F", "Fluorine", 9);
    public static Element Neon = new Element("Ne", "Neon", 10);
    public static Element Sodium = new Element("Na", "Sodium", 11);
    public static Element Magnesium = new Element("Mg", "Magnesium", 12);
    public static Element Aluminum = new Element("Al", "Aluminum", 13);
    public static Element Silicon = new Element("Si", "Silicon", 14);
    public static Element Phosphorus = new Element("P", "Phosphorus", 15);
    public static Element Sulfur = new Element("S", "Sulfur", 16);
    public static Element Chlorine = new Element("Cl", "Chlorine", 17);
    public static Element Argon = new Element("Ar", "Argon", 18);
    public static Element Potassium = new Element("K", "Potassium", 19);
    public static Element Calcium = new Element("Ca", "Calcium", 20);
    public static Element Scandium = new Element("Sc", "Scandium", 21);
    public static Element Titanium = new Element("Ti", "Titanium", 22);
    public static Element Vanadium = new Element("V", "Vanadium", 23);
    public static Element Chromium = new Element("Cr", "Chromium", 24);
    public static Element Manganese = new Element("Mn", "Manganese", 25);
    public static Element Iron = new Element("Fe", "Iron", 26);
    public static Element Cobalt = new Element("Co", "Cobalt", 27);
    public static Element Nickel = new Element("Ni", "Nickel", 28);
    public static Element Copper = new Element("Cu", "Copper", 29);
    public static Element Zinc = new Element("Zn", "Zinc", 30);
    public static Element Gallium = new Element("Ga", "Gallium", 31);
    public static Element Germanium = new Element("Ge", "Germanium", 32);
    public static Element Arsenic = new Element("As", "Arsenic", 33);
    public static Element Selenium = new Element("Se", "Selenium", 34);
    public static Element Bromine = new Element("Br", "Bromine", 35);
    public static Element Krypton = new Element("Kr", "Krypton", 36);
    public static Element Rubidium = new Element("Rb", "Rubidium", 37);
    public static Element Strontium = new Element("Sr", "Strontium", 38);
    public static Element Yttrium = new Element("Y", "Yttrium", 39);
    public static Element Zirconium = new Element("Zr", "Zirconium", 40);
    public static Element Niobium = new Element("Nb", "Niobium", 41);
    public static Element Molybdenum = new Element("Mo", "Molybdenum", 42);
    public static Element Technetium = new Element("Tc", "Technetium", 43);
    public static Element Ruthenium = new Element("Ru", "Ruthenium", 44);
    public static Element Rhodium = new Element("Rh", "Rhodium", 45);
    public static Element Palladium = new Element("Pd", "Palladium", 46);
    public static Element Silver = new Element("Ag", "Silver", 47);
    public static Element Cadmium = new Element("Cd", "Cadmium", 48);
    public static Element Indium = new Element("In", "Indium", 49);
    public static Element Tin = new Element("Sn", "Tin", 50);
    public static Element Antimony = new Element("Sb", "Antimony", 51);
    public static Element Tellurium = new Element("Te", "Tellurium", 52);
    public static Element Iodine = new Element("I", "Iodine", 53);
    public static Element Xenon = new Element("Xe", "Xenon", 54);
    public static Element Cesium = new Element("Cs", "Cesium", 55);
    public static Element Barium = new Element("Ba", "Barium", 56);
    public static Element Lanthanum = new Element("La", "Lanthanum", 57);
    public static Element Cerium = new Element("Ce", "Cerium", 58);
    public static Element Praseodymium = new Element("Pr", "Praseodymium", 59);
    public static Element Neodymium = new Element("Nd", "Neodymium", 60);
    public static Element Promethium = new Element("Pm", "Promethium", 61);
    public static Element Samarium = new Element("Sm", "Samarium", 62);
    public static Element Europium = new Element("Eu", "Europium", 63);
    public static Element Gadolinium = new Element("Gd", "Gadolinium", 64);
    public static Element Terbium = new Element("Tb", "Terbium", 65);
    public static Element Dysprosium = new Element("Dy", "Dysprosium", 66);
    public static Element Holmium = new Element("Ho", "Holmium", 67);
    public static Element Erbium = new Element("Er", "Erbium", 68);
    public static Element Thulium = new Element("Tm", "Thulium", 69);
    public static Element Ytterbium = new Element("Yb", "Ytterbium", 70);
    public static Element Lutetium = new Element("Lu", "Lutetium", 71);
    public static Element Hafnium = new Element("Hf", "Hafnium", 72);
    public static Element Tantalum = new Element("Ta", "Tantalum", 73);
    public static Element Tungsten = new Element("W", "Tungsten", 74);
    public static Element Rhenium = new Element("Re", "Rhenium", 75);
    public static Element Osmium = new Element("Os", "Osmium", 76);
    public static Element Iridium = new Element("Ir", "Iridium", 77);
    public static Element Platinum = new Element("Pt", "Platinum", 78);
    public static Element Gold = new Element("Au", "Gold", 79);
    public static Element Mercury = new Element("Hg", "Mercury", 80);
    public static Element Thallium = new Element("Tl", "Thallium", 81);
    public static Element Lead = new Element("Pb", "Lead", 82);
    public static Element Bismuth = new Element("Bi", "Bismuth", 83);
    public static Element Polonium = new Element("Po", "Polonium", 84);
    public static Element Astatine = new Element("At", "Astatine", 85);
    public static Element Radon = new Element("Rn", "Radon", 86);
    public static Element Francium = new Element("Fr", "Francium", 87);
    public static Element Radium = new Element("Ra", "Radium", 88);
    public static Element Actinium = new Element("Ac", "Actinium", 89);
    public static Element Thorium = new Element("Th", "Thorium", 90);
    public static Element Protactinium = new Element("Pa", "Protactinium", 91);
    public static Element Uranium = new Element("U", "Uranium", 92);
    public static Element Neptunium = new Element("Np", "Neptunium", 93);
    public static Element Plutonium = new Element("Pu", "Plutonium", 94);
    public static Element Americium = new Element("Am", "Americium", 95);
    public static Element Curium = new Element("Cm", "Curium", 96);
    public static Element Berkelium = new Element("Bk", "Berkelium", 97);
    public static Element Californium = new Element("Cf", "Californium", 98);
    public static Element Einsteinium = new Element("Es", "Einsteinium", 99);
    public static Element Fermium = new Element("Fm", "Fermium", 100);
    public static Element Mendelevium = new Element("Md", "Mendelevium", 101);
    public static Element Nobelium = new Element("No", "Nobelium", 102);
    public static Element Lawrencium = new Element("Lr", "Lawrencium", 103);
    public static Element Rutherfordium = new Element("Rf", "Rutherfordium", 104);
    public static Element Dubnium = new Element("Db", "Dubnium", 105);
    public static Element Seaborgium = new Element("Sg", "Seaborgium", 106);
    public static Element Bohrium = new Element("Bh", "Bohrium", 107);
    public static Element Hassium = new Element("Hs", "Hassium", 108);
    public static Element Meitnerium = new Element("Mt", "Meitnerium", 109);
    public static Element Darmstadtium = new Element("Ds", "Darmstadtium", 110);
    public static Element Roentgenium = new Element("Rg", "Roentgenium", 111);
    public static Element Copernicium = new Element("Cn", "Copernicium", 112);
    public static Element Nihonium = new Element("Nh", "Nihonium", 113);
    public static Element Flerovium = new Element("Fl", "Flerovium", 114);
    public static Element Moscovium = new Element("Mc", "Moscovium", 115);
    public static Element Livermorium = new Element("Lv", "Livermorium", 116);
    public static Element Tennessine = new Element("Ts", "Tennessine", 117);
    public static Element Oganesson = new Element("Og", "Oganesson", 118);
    #endregion
    public static Element[] All = {
        Hydrogen,
        Helium,
        Lithium,
        Beryllium,
        Boron,
        Carbon,
        Nitrogen,
        Oxygen,
        Fluorine,
        Neon,
        Sodium,
        Magnesium,
        Aluminum,
        Silicon,
        Phosphorus,
        Sulfur,
        Chlorine,
        Argon,
        Potassium,
        Calcium,
        Scandium,
        Titanium,
        Vanadium,
        Chromium,
        Manganese,
        Iron,
        Cobalt,
        Nickel,
        Copper,
        Zinc,
        Gallium,
        Germanium,
        Arsenic,
        Selenium,
        Bromine,
        Krypton,
        Rubidium,
        Strontium,
        Yttrium,
        Zirconium,
        Niobium,
        Molybdenum,
        Technetium,
        Ruthenium,
        Rhodium,
        Palladium,
        Silver,
        Cadmium,
        Indium,
        Tin,
        Antimony,
        Tellurium,
        Iodine,
        Xenon,
        Cesium,
        Barium,
        Lanthanum,
        Cerium,
        Praseodymium,
        Neodymium,
        Promethium,
        Samarium,
        Europium,
        Gadolinium,
        Terbium,
        Dysprosium,
        Holmium,
        Erbium,
        Thulium,
        Ytterbium,
        Lutetium,
        Hafnium,
        Tantalum,
        Tungsten,
        Rhenium,
        Osmium,
        Iridium,
        Platinum,
        Gold,
        Mercury,
        Thallium,
        Lead,
        Bismuth,
        Polonium,
        Astatine,
        Radon,
        Francium,
        Radium,
        Actinium,
        Thorium,
        Protactinium,
        Uranium,
        Neptunium,
        Plutonium,
        Americium,
        Curium,
        Berkelium,
        Californium,
        Einsteinium,
        Fermium,
        Mendelevium,
        Nobelium,
        Lawrencium,
        Rutherfordium,
        Dubnium,
        Seaborgium,
        Bohrium,
        Hassium,
        Meitnerium,
        Darmstadtium,
        Roentgenium,
        Copernicium,
        Nihonium,
        Flerovium,
        Moscovium,
        Livermorium,
        Tennessine,
        Oganesson
    };

    public static bool TryParse(string s, [MaybeNullWhen((false))] out Element element)
    {
        var result = All.FirstOrDefault(x=>x.Symbol == s || x.Name == s);
        if (result != null)
        {
            element = result;
            return true;
        }
        element = Element.Unknown;
        return false;
    }
}
