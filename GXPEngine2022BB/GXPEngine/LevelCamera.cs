namespace GXPEngine
{
    public class LevelCamera : Camera
    {
        private float boundaryLeft;
        private float boundaryRight;
        private bool dragging = false;
        private Vector2 savedMousePosition;
        public bool canDrag = true;
        private Player target;
        public LevelCamera(float boundaryLeft, float boundaryRight, Player target) : base(0, 0, 1344, 768)
        {
            this.boundaryLeft = boundaryLeft;
            this.boundaryRight = boundaryRight;
            this.target = target;
        }

        void Update()
        {
           
            if (target.isMoving) x = target.x;
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