﻿using System;
using System.Diagnostics;

namespace LittlePipeline
{
    public class Pipeline<TSubject> : IPipeline<TSubject>
        where TSubject : class
    {
        private readonly ITaskFactory factory;
        private TSubject subject;

        public Pipeline(ITaskFactory factory)
        {
            this.factory = factory;
        }

        [DebuggerStepThrough]
        public void Subject(TSubject newSubject)
        {
            subject = newSubject;
        }

        [DebuggerStepThrough]
        public void Do<TTask>()
            where TTask : ITask<TSubject>
        {
            CheckForSubject();

            var task = factory.Create<TTask>();
            task.Run(subject);
        }

        private void CheckForSubject()
        {
            if (subject == null)
                throw new InvalidOperationException("No subject has been set, cannot perform any tasks.");
        }
    }
}
