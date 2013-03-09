using NotamLib.Parsing.Nodes;
using Irony.Parsing;

namespace NotamLib.Parsing
{
    [Language("Simple non-real coordinates", "0.1", "A grammar to parse an imaginary coordinate system")]
    public class CoordinateGrammar : Grammar
    {
        //70 50' 44" N 1 13' 66" E
        public CoordinateGrammar()
        {
            #region Terminals
            var integer = new NumberLiteral("integer", NumberOptions.IntOnly);
            var point = ToTerm(".", "dot");
            var lat = ToTerm("N", "north") | ToTerm("S", "south");
            var lon = ToTerm("E", "east") | ToTerm("W", "west");
            var minuteMarker = ToTerm("'", "minute");
            var secondMarker = ToTerm("\"", "second");
            #endregion

            #region Non-Terminals
            var decimalAmount = new NonTerminal("decimalAmount", typeof(DecimalAmountNode));
            var minute = new NonTerminal("minute", typeof(MinuteNode));
            var second = new NonTerminal("second", typeof(SecondNode));

            var imperialMagnitude = new NonTerminal("decimalMagnitude", typeof (ImperialMagnitudeNode));

            var imperialLatitude = new NonTerminal("imperialLatitude", typeof (ImperialLatitudeNode));
            var imperialLongitude = new NonTerminal("imperialLongitude", typeof (ImperialLongitudeNode));
            var imperialCoordinate = new NonTerminal("imperialCoordinate", typeof(ImperialCoordinateNode));
            #endregion

            #region Rules
            decimalAmount.Rule = integer | integer + point + integer;

            minute.Rule = integer + minuteMarker;
            second.Rule = integer + secondMarker;
            imperialMagnitude.Rule = integer + minute + second;
            imperialLatitude.Rule = imperialMagnitude + lat;
            imperialLongitude.Rule = imperialMagnitude + lon;
            imperialCoordinate.Rule = imperialLatitude + imperialLongitude;
            #endregion

            Root = imperialCoordinate;
        }
    }
}