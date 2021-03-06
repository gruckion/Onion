﻿using System;

namespace Onion.Data
{
	public interface IAuditableEntity
	{
		DateTimeOffset CreatedDate { get; set; }

		string CreatedBy { get; set; }

		DateTimeOffset UpdatedDate { get; set; }

		string UpdatedBy { get; set; }
	}
}