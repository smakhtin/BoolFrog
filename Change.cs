using System;
using Grasshopper.Kernel;

namespace BoolFrog
{
	public class Change : GH_Component
	{
		#region inputs
		private const string PInInput = "Input";
		#endregion inputs
		
		#region outputs
		private const string POutOnChange = "OnChange";
		#endregion outputs

		private double FPInput;

		public Change() : base("Change Value", "Change", "Send true when incoming value is changed", "BoolFrog", "Animation")
		{
			
		}

		public override Guid ComponentGuid
		{
			get { return new Guid("768a9e39-e65a-4410-9f96-d4222091bde1"); }
		}

		protected override void RegisterInputParams(GH_InputParamManager pManager)
		{
			pManager.Register_DoubleParam(PInInput, "Inp", "Input value", 0.0);
		}

		protected override void RegisterOutputParams(GH_OutputParamManager pManager)
		{
			pManager.Register_BooleanParam(POutOnChange, "Out", "Output");
		}

		protected override void SolveInstance(IGH_DataAccess da)
		{
			GH_Document d = OnPingDocument();
			if (d == null) return;

			double input = 0;

			if (!da.GetData(PInInput, ref input)) { return; }

			bool valueChanged = Math.Abs(input - FPInput) > 0.001;
			
			if(valueChanged)
			{
				da.SetData(POutOnChange, true);
				d.ScheduleSolution(10, CallBack);
			}
			else
			{
				da.SetData(POutOnChange, false);
			}

			FPInput = input;
		}

		private void CallBack(GH_Document doc)
		{
			ExpireSolution(false);
		}
	}
}
