using CentralServer.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer
{
    /// <summary>
    /// Контекст базы данных RemoteTasks. Отвечает за доступ к объектам типа DbSet, соответствующим таблицам базы данных и сохранение данных 
    /// </summary>
    public class CsContext : DbContext
    {

        /// <summary>
        /// Конструктор контекста
        /// </summary>
        /// <param name="connectionString">Строка подключения контекста к базе данных</param>
        /// <param name="commandTimeout">Время ожидания выполнения команды</param>
        public CsContext(string connectionString, int commandTimeout) : base(connectionString)
        {
            if (commandTimeout > 15)
            {
                this.Database.CommandTimeout = commandTimeout;
            }
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            //Переопределяем метод создания модели для отключения каскадного удаления
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        /// <summary>
        /// Перечень компьютеров, над которыми осуществляется удалённое администрирование
        /// </summary>
        public DbSet<Computer> Computers { get; set; }
        
        /// <summary>
        /// Перечень задач, которые должен выполнять тот или иной удалённый компьютер
        /// </summary>
        public DbSet<DataObjects.Task> Tasks { get; set; }

        public DbSet<TaskResult> TaskResults { get; set; }
    }
}

