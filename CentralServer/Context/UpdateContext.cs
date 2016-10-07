using CentralServer.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer
{
    /// <summary>
    /// Контекст базы данных для создания базы и обновления её структуры
    /// </summary>
    public class UpdateContext : CsContext
    {

        public UpdateContext() : this(SQLSettings.CsConnectionString(), SQLSettings.CommandTimeout())
        {
            //Необходимо обязательно иметь конструктор по умолчанию для обработчика изменений базы
        }

        private string _ConnectionString;

        private int _CommandTimeout;

        public UpdateContext(string ConnectionString, int CommandTimeout) : base(ConnectionString, 0)
        {
            _ConnectionString = ConnectionString;
            _CommandTimeout = CommandTimeout;
            if (this.Database.Exists())
            {
                if (CommandTimeout > 15)
                {
                    this.Database.CommandTimeout = CommandTimeout;
                }
            }

            if (this.Database.Exists())
            {
                System.Data.Entity.Database.SetInitializer<UpdateContext>(new MigrateDatabaseToLatestVersion<UpdateContext, CsContextMigrationsToNextVersion>());
            }
            else
            {
                this.Database.Create();
                System.Data.Entity.Database.SetInitializer<UpdateContext>(new MigrateDatabaseToLatestVersion<UpdateContext, CsContextMigrationsToNextVersion>());
            }
        }
    }

    /// <summary>
    /// Не менять название класса, от этого зависит ContextKey в таблице _MigrationHistory базы данных
    /// </summary>
    public class CsContextMigrationsToNextVersion : DbMigrationsConfiguration<UpdateContext>
    {

        public CsContextMigrationsToNextVersion() : base()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            this.CommandTimeout = 300;
        }
        
        protected override void Seed(UpdateContext context)
        {
            base.Seed(context);
            //Переопределяем метод для более тонкой настройки обновлений
            //Try
            //    context.Database.ExecuteSqlCommand("CREATE INDEX IX_CountOfSalesByCustomer ON Documents (Discriminator, DateOfApprove, CustomerId) ")
            //Catch
            //End Try

            //Try
            //    context.Database.ExecuteSqlCommand("CREATE INDEX IX_CheckPrevMarketingToolRecords ON MarketingToolRecords (Discriminator, MarketingToolForPeriodId, CustomerId, StartDate, EndDate) ")
            //Catch
            //End Try

        }
    }
}
