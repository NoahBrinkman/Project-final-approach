namespace GXPEngine
{
    public class LevelCamera : Camera
    {
        private float boundaryLeft;
        private float boundaryRight;
        private bool dragging = false;
        private Vector2 savedMousePosition;
        public bool canDrag = true;
        public LevelCamera(float boundaryLeft, float boundaryRight) : base(0, 0, 1344, 768)
        {
            this.boundaryLeft = boundaryLeft;
            this.boundaryRight = boundaryRight;
        }

        void Update()
        {
            x = Mathf.Clamp(x, boundaryLeft, boundaryRight);
            if(!canDrag) return;
            if (Input.GetMouseButtonDown(0))
            {
                //start dragging
                dragging = true;
            }
            if (dragging)
            {
                //set the position but clamp it between the boundaries
                x += savedMousePosition.x - Input.mouseX;
            }
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
            }

            savedMousePosition = new Vector2(Input.mouseX, Input.mouseY);
        }
    }
}