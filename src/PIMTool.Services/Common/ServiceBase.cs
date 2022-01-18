using AutoMapper;
using PIMTool.Services.Common.Exception;
using PIMTool.Shared.Extension.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Common
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : EntityBase
    {
        protected void CheckAndThrowIfObjectModified(TEntity entity, byte[] candidateVersion)
        {
            if (entity == null)
            {
                return;
            }
            if (!CheckIfObjectNotModified(candidateVersion, entity.RowVersion))
            {
                string oldVersion = StringHelper.GetString(candidateVersion);
                string newVersion = StringHelper.GetString(entity.RowVersion);
                throw new CoreException.DataHasBeenModifiedException(entityName: nameof(TEntity),
                    id: entity.Id, oldVersion: oldVersion, newVersion: newVersion);
            }
        }

        protected void CheckAndThrowIfObjectModified(TEntity entity, string candidateVersion)
        {
            if (entity == null)
            {
                return;
            }

            if (!CheckIfObjectNotModified(candidateVersion, entity.RowVersion))
            {
                string newVersion = StringHelper.GetString(entity.RowVersion);
                throw new CoreException.DataHasBeenModifiedException(entityName: nameof(TEntity),
                    id: entity.Id, oldVersion: candidateVersion, newVersion: newVersion);
            }
        }

        protected bool CheckIfObjectNotModified(byte[] candidateVersion, byte[] currentVersion)
        {
            string candidateVerStr = StringHelper.GetString(candidateVersion);
            string currentVerStr = StringHelper.GetString(currentVersion);
            return candidateVerStr == currentVerStr;
        }

        protected bool CheckIfObjectNotModified(string candidateVersion, byte[] currentVersion)
        {
            string currentVerStr = StringHelper.GetString(currentVersion);
            return candidateVersion == currentVerStr;
        }
    }
}
