﻿using System;

namespace Waher.Networking.WHOIS
{
	public partial class WhoIsClient
	{
		private static readonly WhoIsIpv4ServiceEnum[] ipv4ToWhoIsService = new WhoIsIpv4ServiceEnum[]
		{
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_afrinic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_afrinic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_afrinic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_afrinic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_afrinic_net,
			WhoIsIpv4ServiceEnum.whois_afrinic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_lacnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_arin_net,
			WhoIsIpv4ServiceEnum.whois_ripe_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.whois_apnic_net,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined,
			WhoIsIpv4ServiceEnum.undefined
		};
	}
}
