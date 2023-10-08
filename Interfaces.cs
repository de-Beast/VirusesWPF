using System;
using System.Collections.Generic;

namespace Viruses;

internal interface IForKurs
{
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
