﻿namespace Todo_Assignment.API.Services
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException() 
        {
            
        }

        public TaskNotFoundException(string message) : base(message)
        {

        }

        public TaskNotFoundException(string message, Exception inner): base(message, inner)
        { 

        }
    }
}
