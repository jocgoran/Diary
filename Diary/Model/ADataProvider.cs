using Diary.View;
using System.Collections;

namespace Diary.Model
{
    abstract class ADataProvider
    {
        private ArrayList viewers = new ArrayList();

        public void Subscribe(IViewer viewer)
        {
            viewers.Add(viewer);
        }

        public void Unsubscribe(IViewer viewer)
        {
            viewers.Remove(viewer);
        }

        public void Render(string TableName)
        {
            foreach (IViewer v in viewers)
            {
                v.Render(TableName);
            }
        }

    }
}