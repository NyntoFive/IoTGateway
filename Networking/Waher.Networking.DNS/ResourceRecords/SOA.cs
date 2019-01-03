﻿using System;
using System.Collections.Generic;
using System.IO;
using Waher.Networking.DNS.Enumerations;

namespace Waher.Networking.DNS.ResourceRecords
{
	/// <summary>
	/// Start Of zone Authority
	/// </summary>
	public class SOA : ResourceRecord
	{
		private readonly string mName;
		private readonly string rName;
		private readonly uint serial;
		private readonly uint refresh;
		private readonly uint retry;
		private readonly uint expire;
		private readonly uint minimum;

		/// <summary>
		/// Start Of zone Authority
		/// </summary>
		/// <param name="Name">Name</param>
		/// <param name="Type">Resource Record Type</param>
		/// <param name="Class">Resource Record Class</param>
		/// <param name="Ttl">Time to live</param>
		/// <param name="Data">RR-specific binary data.</param>
		public SOA(string Name, TYPE Type, CLASS Class, uint Ttl, Stream Data) 
			: base(Name, Type, Class, Ttl)
		{
			this.mName = DnsResolver.ReadName(Data);
			this.rName= DnsResolver.ReadName(Data);
			this.serial = DnsResolver.ReadUInt32(Data);
			this.refresh = DnsResolver.ReadUInt32(Data);
			this.retry = DnsResolver.ReadUInt32(Data);
			this.expire = DnsResolver.ReadUInt32(Data);
			this.minimum = DnsResolver.ReadUInt32(Data);
		}

		/// <summary>
		/// Name server that was the original or primary source of data for this zone
		/// </summary>
		public string MName => this.mName;

		/// <summary>
		/// Specifies the mailbox of the person responsible for this zone
		/// </summary>
		public string RName => this.rName;

		/// <summary>
		/// Version number of the original copy of the zone.
		/// </summary>
		public uint Serial => this.serial;

		/// <summary>
		/// Time interval before the zone should be refreshed
		/// </summary>
		public uint Refresh => this.refresh;

		/// <summary>
		/// Interval that should elapse before a failed refresh should be retried
		/// </summary>
		public uint Retry => this.retry;

		/// <summary>
		/// Specifies the upper limit on the time interval that can elapse before 
		/// the zone is no longer authoritative
		/// </summary>
		public uint Expire => this.expire;

		/// <summary>
		/// Minimum TTL field that should be exported with any RR from this zone
		/// </summary>
		public uint Minimum => this.minimum;

		/// <summary>
		/// <see cref="Object.ToString()"/>
		/// </summary>
		public override string ToString()
		{
			return base.ToString() + "\t" + this.mName + "\t" + this.rName +
				"\t" + this.serial + "\t" + this.refresh + "\t" + this.retry + 
				"\t" + this.expire + "\t" + this.minimum;
		}
	}
}