﻿// <auto-generated />

using System.CodeDom.Compiler;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace ToDoList.DAL.Migrations
{
    [GeneratedCode("EntityFramework.Migrations", "6.4.4")]
    public sealed partial class TaskUpdate : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(TaskUpdate));
        
        string IMigrationMetadata.Id
        {
            get { return "202012142320519_task_update"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
