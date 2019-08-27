using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OnlineRecLeague.Regions
{
	public interface IRegionStore
	{
		IRegion FindRegionOrThrow(string regionId);
		bool TryFindRegion(string regionId, out IRegion region);
	}

	public class RegionStore : IRegionStore
	{
		public RegionStore(IReadOnlyDictionary<string, IRegion> regionsByRegionId = null)
		{
			_regionsByRegionId = regionsByRegionId ?? _lazyStaticRegionsByRegionId.Value;
		}

		public IRegion FindRegionOrThrow(string regionId)
		{
			if (!TryFindRegion(regionId, out var region))
			{
				throw new NotSupportedException($"Unknown Region ${regionId}");
			}

			return region;
		}

		public bool TryFindRegion(string regionId, out IRegion region)
		{
			return _regionsByRegionId.TryGetValue(regionId, out region);
		}

		private static IReadOnlyDictionary<string, IRegion> LoadRegionsFromEmbeddedResource()
		{
			using (var streamReader = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream("OnlineRecLeague.Regions.AllRegions.json")))
			{
				return JsonConvert
					.DeserializeObject<IReadOnlyList<Region>>(streamReader.ReadToEnd()).Cast<IRegion>()
					.ToDictionary(region => region.RegionId, region => region);
			}
		}

		private readonly IReadOnlyDictionary<string, IRegion> _regionsByRegionId;

		private static readonly Lazy<IReadOnlyDictionary<string, IRegion>> _lazyStaticRegionsByRegionId = new Lazy<IReadOnlyDictionary<string, IRegion>>(() => LoadRegionsFromEmbeddedResource());
	}
}
