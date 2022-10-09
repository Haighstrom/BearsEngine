namespace BearsEngine.Worlds.Graphics.Text.Components
{
    internal class Line
    {
        private bool _finalised = false;
        private readonly List<ILineComponent> _components = new();
        

        public bool EndsWithNewLine { get; private set; }
        

        public bool IsEmpty => _components.Count == 0;

        public int Spaces => _components.Where(c => c is LC_Space).Count();

        public float Length => _components.Sum(c => c.Length);

        public float LengthExcludingSpaces => _components.Where(c => c is LC_Word).Sum(w => w.Length);

        public float Height => _components.Max(c => c.Height);
        

        public void Add(ILineComponent c)
        {
            if (_finalised)
                throw new Exception($"Line Finalised but trying to add {c} to it");

            _components.Add(c);
        }

        public void Finalise(bool lastCharIsNewline)
        {
            if (_finalised)
                throw new InvalidOperationException("Line was already finalised");

            EndsWithNewLine = lastCharIsNewline;

            if (_components.Count > 1) //don't remove spaces if only a space
                while (_components.Last() is LC_Space s)
                    _components.Remove(s);

            _finalised = true;
        }

        public IEnumerator<ILineComponent> GetEnumerator() => _components.GetEnumerator();
        
    }
}