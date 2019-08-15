using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Onion.Data;
using System;
using System.Linq;

namespace Onion.Repositories.Extensions
{
	public static class ChangeTrackerExtension
	{
		public static void ApplyAuditInformation(this ChangeTracker changeTracker, ICurrentPrincipalAccessor currentPrincipalAccessor)
		{
			var modifiedEntries = changeTracker.Entries()
				.Where(x => x.Entity is IAuditableEntity
							&& (x.State == EntityState.Added || x.State == EntityState.Modified));

			foreach (var entry in modifiedEntries)
			{
				if (!(entry.Entity is IAuditableEntity entity))
					continue;

				string identityName = currentPrincipalAccessor.CurrentPrincipal.Identity.Name;

				if (identityName == null)
					continue;

				//TODO ensure that this DateTimeOffset.UtcNow functions in the same way as DateTime
				var now = DateTimeOffset.UtcNow;

				if (entry.State == EntityState.Added)
				{
					entity.CreatedBy = identityName;
					entity.CreatedDate = now;
				}
				else
				{
					changeTracker.Context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
					changeTracker.Context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
				}

				entity.UpdatedBy = identityName;
				entity.UpdatedDate = now;
			}

		}
	}
}
