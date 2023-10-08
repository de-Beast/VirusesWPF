using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Viruses;

internal abstract class Microorganism : IReproducible
{
	public required string Name { get; init; }

	public abstract int ReproductionSpeed { get; set; }

	[JsonInclude]
	public int DescendantsAmount { get; private set; }

	public string Reproduce()
	{
		int old_descendant_amount = DescendantsAmount;
		DescendantsAmount += ReproductionSpeed;
		return ReproduceString(old_descendant_amount);
	}

	protected abstract string ReproduceString(int old_descendant_amount);
}

internal class Cell : Microorganism, IForKurs
{
	[DefaultValue(1)]
	public override int ReproductionSpeed { get; set; } = 1;

	protected override string ReproduceString(int old_descendant_amount)
	{
		return $"{Name}: Old amount of cell descendants: {old_descendant_amount}; New amount of cell descendants: {DescendantsAmount}";
	}

	public string Eat()
	{
		return $"{Name}: Cell has eaten";
	}

	public override string ToString()
	{
		return $"{Name}: Cell has {DescendantsAmount} descendants with reproduction speed of {ReproductionSpeed}";
	}

	public List<Func<string>> GenerateDelegateList()
	{
		return new() { Reproduce, Eat };
	}
}

internal abstract class Virus : Microorganism, IKill
{
	[JsonInclude]
	public bool DidKill { get; protected set; }

	public abstract int Lethality { get; }

	public virtual string Kill()
	{
		if (DidKill)
			return $"{Name}: Virus has already killed the last infected organism";

		DidKill = true;
		return $"{Name}: Virus has killed the last infected organism";
	}

	public virtual string Infect()
	{
		DidKill = false;
		return $"{Name}: New organism is infected";
	}
}

internal sealed class Coronavirus : Virus, IForKurs
{
	[DefaultValue(4)]
	public override int ReproductionSpeed { get; set; } = 4;

	[JsonInclude]
	public uint InfectedOrganisms { get; private set; } = 1;

	[JsonIgnore]
	public override int Lethality => 5;

	public static string StartPandemia()
	{
		return "Ladies and Gentlemen, COVID-19 has started";
	}

	public override string Infect()
	{
		InfectedOrganisms++;
		return base.Infect();
	}

	public override string Kill()
	{
		if (InfectedOrganisms == 0)
		{
			return DidKill ? base.Kill() : $"{Name}: The last infected organism have been already cured";
		}

		InfectedOrganisms--;
		return InfectedOrganisms == 0 ? base.Kill() : $"{Name}: Coronavirus has killed one of infected organism";
	}

	public string UseMedicine()
	{
		if (InfectedOrganisms > 0)
		{
			InfectedOrganisms--;
			return InfectedOrganisms == 0
				? $"{Name}: The last infected organism has been cured"
				: $"{Name}: One of infected organism has been cured";
		}

		return $"{Name}: There are no organisms to cure";
	}

	protected override string ReproduceString(int old_descendant_amount)
	{
		return $"{Name}: Old amount of Coronavirus descendants: {old_descendant_amount}; New amount of Coronavirus descendants: {DescendantsAmount}";
	}

	public override string ToString()
	{
		return $"{Name}: Coronavirus has {DescendantsAmount} descendants with reproduction speed of {ReproductionSpeed};\n" +
		       $"Lethality is level {Lethality};\n" +
		       (InfectedOrganisms == 0
			       ? $"There are no infected organisms; The last organism was {(DidKill ? "killed" : "cured")}"
			       : $"There are/is {InfectedOrganisms} infected organism(-s)"
		       );
	}

	public List<Func<string>> GenerateDelegateList()
	{
		return new() { StartPandemia, Kill, Infect, UseMedicine, Reproduce };
	}
}

internal sealed class Influenza : Virus, IForKurs
{
	[DefaultValue(3)]
	public override int ReproductionSpeed { get; set; } = 3;

	[JsonInclude]
	public uint Mutations { get; private set; }

	[JsonIgnore]
	public override int Lethality => 3;

	protected override string ReproduceString(int old_descendant_amount)
	{
		return $"{Name}: Old amount of Influenza descendants: {old_descendant_amount}; New amount of Influenza descendants: {DescendantsAmount}";
	}

	public string Mutate()
	{
		Mutations++;
		return $"Influenza {Name} has mutated and now has {Mutations} mutations";
	}

	public override string ToString()
	{
		return $"{Name}: Influenza has {DescendantsAmount} descendants with reproduction speed of {ReproductionSpeed};\n" +
		       $"Lethality is level {Lethality};\nIt has {Mutations} mutations;\nInfected organism is {(DidKill ? "" : "not ")}killed";
	}

	public List<Func<string>> GenerateDelegateList()
	{
		return new() { Kill, Infect, Reproduce, Mutate };
	}
}
