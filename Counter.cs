using System;
using Grasshopper.Kernel;

namespace BoolFrog
{
	public class Counter : GH_Component
	{
		#region inputs
		private const string PInUp = "Up";
		private const string PInDown = "Down";
		private const string PInMinimum = "Minimum";
		private const string PInMaximum = "Maximum";
		private const string PInIncrement = "Increment";
		private const string PInDefault = "Default";
		private const string PInReset = "Reset";
		#endregion inputs

		#region outputs
		private const string POutOutput = "Output";
		#endregion outputs

		private double FCounter;

		public Counter():base("Counter", "Counter", "Counts up and down", "BoolFrog", "Animation")
		{
			
		}

		public override Guid ComponentGuid
		{
			get { return new Guid("5354e39b-a559-4539-86b5-2312f34e8f03"); }
		}

		protected override void RegisterInputParams(GH_InputParamManager pManager)
		{
			pManager.Register_BooleanParam(PInUp, "U", "Up value by increment", false);
			pManager.Register_BooleanParam(PInDown, "D", "Down value by increment", false);
			pManager.Register_DoubleParam(PInMinimum, "Min", "Minimum value", 0.0);
			pManager.Register_DoubleParam(PInMaximum, "Max", "Maximum value", 15.0);
			pManager.Register_DoubleParam(PInIncrement, "Inc", "Increment value", 1.0);
			pManager.Register_DoubleParam(PInDefault, "D", "Value, that set after counter reset", 0.0);
			pManager.Register_BooleanParam(PInReset, "R", "Reset counter", false);
		}

		protected override void RegisterOutputParams(GH_OutputParamManager pManager)
		{
			pManager.Register_DoubleParam(POutOutput, "Out", "Counter value");
		}

		protected override void SolveInstance(IGH_DataAccess da)
		{
			//Process input
			double increment = 0;
			da.GetData(PInIncrement, ref increment);

			bool up = false;
			da.GetData(PInUp, ref up);
			if (up) FCounter += increment;

			bool down = false;
			da.GetData(PInDown, ref down);
			if (down) FCounter -= increment;

			double defaultValue = 0.0;
			da.GetData(PInDefault, ref defaultValue);

			bool reset = false;
			da.GetData(PInReset, ref reset);
			if (reset) FCounter = defaultValue;

			double min = 0.0;
			da.GetData(PInMinimum, ref min);
			if (FCounter < min) FCounter = min;

			double max = 0.0;
			da.GetData(PInMaximum, ref max);
			if (FCounter > max) FCounter = max;

			//Output
			da.SetData(POutOutput, FCounter);
		}
	}
}
