using System;
using System.Diagnostics;
using System.Timers;
using Grasshopper.Kernel;

namespace BoolFrog
{
	public class Delay :GH_Component
	{
		#region inputs
		private const string PInInput = "Input";
		private const string PInDelay = "Delay";
		#endregion inputs

		#region outputs
		private const string POutOutput = "Output";
		#endregion outputs

		private readonly Timer FTimer;
		private double FInputValue;
		private double FOutputValue;

		private bool FTimerComplete;

		public Delay():base("Delay", "Delay", "Set output value with delay", "BoolFrog", "Animation")
		{
			FTimer = new Timer(1000) {AutoReset = false};
			FTimer.Elapsed += OnTimerElapsed;
		}

		private void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			Debug.Print("Timer elapsed");
			FOutputValue = FInputValue;
			FTimerComplete = true;
			ExpireSolution(true);
		}

		public override Guid ComponentGuid
		{
			get { return new Guid("21d61adb-4d2d-431b-bfad-b416ba7769c7"); }
		}

		protected override void RegisterInputParams(GH_InputParamManager pManager)
		{
			pManager.Register_DoubleParam(PInInput, "Inp", "Input value", 0.0);
			pManager.Register_IntegerParam(PInDelay, "D", "Value changing delay in milliseconds", 1000);
		}

		protected override void RegisterOutputParams(GH_OutputParamManager pManager)
		{
			pManager.Register_DoubleParam(POutOutput, "Out", "Output value");
		}

		protected override void SolveInstance(IGH_DataAccess da)
		{
			double input = 0.0;
			da.GetData(PInInput, ref input);
			FInputValue = input;

			int delay = 1000;
			da.GetData(PInDelay, ref delay);
			FTimer.Interval = delay;

			if (FTimerComplete)
			{
				da.SetData(POutOutput, FOutputValue);
			}
			else
			{
				FTimerComplete = false;
				FTimer.Start();
			}
		}
	}
}
