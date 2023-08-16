using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Viruses;

internal abstract class Microorganism : IReproducible, IForKurs
{
	public required string Name { get; init; }

	public abstract int ReproductionSpeed { get; set; }

	[JsonInclude]
	public int DescendantsAmount { get; private set; }

	public abstract List<Func<string>> GenerateDelegateList();

	public string Reproduce()
	{
		int old_descendant_amount = DescendantsAmount;
		DescendantsAmount += ReproductionSpeed;
		return ReproduceString(old_descendant_amount);
	}

	protected abstract string ReproduceString(int old_descendant_amount);

	public abstract string Mutate();

}

internal class Cell : Microorganism
{
	[DefaultValue(1)]
	public override int ReproductionSpeed { get; set; } = 1;

	protected override string ReproduceString(int old_descendant_amount)
	{
		return $"{Name}: Old amount of cell descendants: {old_descendant_amount}; New amount of cell descendants: {DescendantsAmount}";
	}

	public override string Mutate()
	{
		return $"{Name}: Cell has mutated";
	}

	public string Eat()
	{
		return $"{Name}: Cell has eaten";
	}

	public override string ToString()
	{
		return $"{Name}: Cell has {DescendantsAmount} descendants with reproduction speed of {ReproductionSpeed}";
	}

	public override List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { Reproduce, Mutate, Eat };
	}
}

internal abstract class Virus : Microorganism, IKill
{
	[JsonInclude]
	public bool DidKill { get; protected set; }

	public abstract int Lethality { get; }

	public string Kill()
	{
		if (DidKill)
			return $"{Name} virus has already killed the victim";

		DidKill = true;
		return $"{Name} virus has killed the victim";
	}

	public override string ToString()
	{
		return $"{Name}: Virus has {DescendantsAmount} descendants with reproduction speed of {ReproductionSpeed}; " +
			   $"Lethality is level {Lethality}; Infected organism is {(DidKill ? "" : "not ")}killed";
	}

	public string Infect()
	{
		DidKill = false;
		return $"{Name}: New organism is infected";
	}

}

internal sealed class Coronavirus : Virus
{
	[DefaultValue(4)]
	public override int ReproductionSpeed { get; set; } = 4;

	[JsonIgnore]
	public override int Lethality => 5;

	public static string StartPandemia()
	{
		return "Ladies and Gentlemen, COVID-19 has started";
	}

	protected override string ReproduceString(int old_descendant_amount)
	{
		return $"{Name}: Old amount of Coronavirus descendants: {old_descendant_amount}; New amount of Coronavirus descendants: {DescendantsAmount}";
	}

	public override string Mutate()
	{
		return $"Coronavirus {Name} has mutated";
	}

	public override List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { StartPandemia, Kill, Infect, Reproduce, Mutate };
	}
}

internal sealed class Influenza : Virus
{
	[DefaultValue(3)]
	public override int ReproductionSpeed { get; set; } = 3;


	[JsonIgnore]
	public override int Lethality => 3;

	protected override string ReproduceString(int old_descendant_amount)
	{
		return $"{Name}: Old amount of Influenza descendants: {old_descendant_amount}; New amount of Influenza descendants: {DescendantsAmount}";
	}

	public override string Mutate()
	{
		return $"Influenza {Name} has mutated";
	}

	public override List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { Kill, Infect, Reproduce, Mutate };
	}
}
