namespace Viruses;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

public class Rabbit : IReproducible, IForKurs
{
	public enum ESex
	{
		MALE,
		FEMALE
	}

	public required string Name { get; init; }

	[DefaultValue(3)]
	public int ReproductionSpeed { get; set; } = 3;

	[JsonInclude]
	public int DescendantsAmount { get; protected set; }

	public required ESex Sex { get; init; }

	public string Reproduce()
	{
		int old_descendant_amount = DescendantsAmount;
		DescendantsAmount += ReproductionSpeed;
		return $"{Name}: Old amount of rabbit descendants - {old_descendant_amount}; New amount of rabbit descendants - {DescendantsAmount}";
	}

	public override string ToString()
	{
		return $"{Name}: {Sex.ToString().ToLower()} rabbit has {DescendantsAmount} descendants and reproduction speed of {ReproductionSpeed}";
	}

	public List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { Reproduce };
	}
}
