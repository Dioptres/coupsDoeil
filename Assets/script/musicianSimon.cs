using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicianSimon : Lookable {

	public singerSimon actualSinger;
	int numberOfMyInstrument;

	public enum Instrument
	{
		None = 0,
		Bass = 1,
		Flute = 2,
		Guitare = 4,
		Percu = 8,
		Uku = 16
	}

	public Instrument instrument;

	protected override void StartLookable ()
	{
		base.StartLookable ();

		switch(instrument)
		{
			case Instrument.Bass:
				numberOfMyInstrument = 1;
				break;
			case Instrument.Flute:
				numberOfMyInstrument = 2;
				break;
			case Instrument.Guitare:
				numberOfMyInstrument = 4;
				break;
			case Instrument.Uku:
				numberOfMyInstrument = 3;
				break;
		}
	}

	protected override void UpdateLookable ()
	{
		base.UpdateLookable ();
	}

	public override void DoAction ()
	{
		base.DoAction ();
		Debug.Log ("choosen !   " + numberOfMyInstrument);
		actualSinger.choose (numberOfMyInstrument);
	}

}
