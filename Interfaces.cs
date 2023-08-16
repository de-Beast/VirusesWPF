using System;
using System.Collections.Generic;

namespace Viruses;

internal interface IForKurs
{
	string Name { get; init; }

	List<Func<string>> GenerateDelegateList();
}

internal interface IReproducible
{
	int ReproductionSpeed { get; set; }
	int DescendantsAmount { get; }

	string Reproduce();
}

internal interface IKill
{
	bool DidKill { get; }

	string Kill();
}
