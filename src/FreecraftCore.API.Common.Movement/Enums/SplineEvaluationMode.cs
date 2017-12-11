using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace FreecraftCore.API.Common
{
	//Same on 1.12.1
	/// <summary>
	/// Enumeration of all spline evalutation modes.
	/// Based on Jackpoz's SplineEvalutationMode and Trinitycore's SplineBase.EvaluationMode in Spline.h
	/// </summary>
	[WireDataContract]
	public enum SplineEvaluationMode : byte
	{
		//TODO: Document
		ModeLinear,
		ModeCatmullrom,
		ModeBezier3_Unused,
		UninitializedMode,
		ModesEnd
	}
}
