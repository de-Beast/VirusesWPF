using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Viruses;

public class Hitman : IKill, IForKurs
{
	public required string Name { get; init; }

	[JsonInclude]
	public bool DidKill { get; protected set; }

	private string _victim = null!;

	public required string Victim
	{
		get => _victim;
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				return;

			if (_victim != null!)
				DidKill = false;

			_victim = value;
		}

	}

	public string Kill()
	{
		if (DidKill)
			return $"The victim is already killed by {Name} hitman";

		DidKill = true;
		return $"{Name} hitman has killed the victim";
	}

	public override string ToString()
	{
		return $"{Name}: {Victim} is {(!DidKill ? "not " : "")}killed";
	}

	public List<Func<string>> GenerateDelegateList()
	{
		return new() { Kill };
	}
}
