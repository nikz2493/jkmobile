using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
namespace JKMIOSApp
{
    public class OxyPlotServices
    {

        public PlotModel CreatePlotModel()
        {
            var plotModel = new PlotModel { Title = "OxyPlot Demo" };

            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 10, Minimum = 0 });

            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            series1.Points.Add(new DataPoint(0.0, 6.0));
            series1.Points.Add(new DataPoint(1.4, 2.1));
            series1.Points.Add(new DataPoint(2.0, 4.2));
            series1.Points.Add(new DataPoint(3.3, 2.3));
            series1.Points.Add(new DataPoint(4.7, 7.4));
            series1.Points.Add(new DataPoint(6.0, 6.2));
            series1.Points.Add(new DataPoint(8.9, 8.9));

            plotModel.Series.Add(series1);

            return plotModel;
        }


        public PlotModel CreatePIModel()
        {
            var plotModel = new PlotModel { Title = "" };

            var seriesP1 = new PieSeries { StrokeThickness = 0.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = -240, Diameter = 0.95, InnerDiameter = 0.6 };
            var seriesP2 = new PieSeries { StrokeThickness = 0.0, InsideLabelPosition = 0.3, AngleSpan = 360, StartAngle = -240, Diameter = 1.0, InnerDiameter = 0.95, ToolTip = "" };

            seriesP1.Slices.Add(new PieSlice("A", 15) { IsExploded = false, Fill = OxyColor.FromRgb(232, 144, 165) });
            seriesP2.Slices.Add(new PieSlice("", 15) { IsExploded = false, Fill = OxyColor.FromRgb(236, 61, 98) });

            seriesP1.Slices.Add(new PieSlice("B", 25) { IsExploded = true, Fill = OxyColor.FromRgb(138, 219, 222) });
            seriesP2.Slices.Add(new PieSlice("", 25) { IsExploded = true, Fill = OxyColor.FromRgb(50, 212, 212) });

            seriesP1.Slices.Add(new PieSlice("C", 35) { IsExploded = true, Fill = OxyColor.FromRgb(124, 139, 166) });
            seriesP2.Slices.Add(new PieSlice("", 35) { IsExploded = true, Fill = OxyColor.FromRgb(22, 51, 99) });

            seriesP1.Slices.Add(new PieSlice("D", 20) { IsExploded = true, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 20) { IsExploded = true, Fill = OxyColor.FromRgb(204, 208, 223) });

            seriesP1.Slices.Add(new PieSlice("E", 5) { IsExploded = true, Fill = OxyColor.FromRgb(227, 227, 233) });
            seriesP2.Slices.Add(new PieSlice("", 5) { IsExploded = true, Fill = OxyColor.FromRgb(230, 217, 220) });

            plotModel.Series.Add(seriesP1);
            plotModel.Series.Add(seriesP2);

            seriesP1.OutsideLabelFormat = "";
            seriesP1.TickHorizontalLength = 0.00;
            seriesP1.TickRadialLength = 0.00;

            seriesP2.OutsideLabelFormat = "";
            seriesP2.TickHorizontalLength = 0.00;
            seriesP2.TickRadialLength = 0.00;

            return plotModel;
        }

    }
}

