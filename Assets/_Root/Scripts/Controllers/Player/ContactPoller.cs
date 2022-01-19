using UnityEngine;


namespace _Root.Scripts.Controllers
{
    public class ContactPoller
    {
        #region Fields

        private readonly ContactPoint2D[] _contactPoints = new ContactPoint2D[10];
        private readonly Collider2D _collider;
        private const float _collisionTresh = 0.5f;
        private int _contactsCount;

        #endregion

        #region Properties

        public bool IsGrounded { get; private set; }
        public bool HasLeftContact { get; private set; }
        public bool HasRightContact { get; private set; }

        #endregion


        #region Constructor

        public ContactPoller(Collider2D collider)
        {
            _collider = collider;
        }

        #endregion


        #region Methods

        public void UpdateContacts()
        {
            IsGrounded = false;
            HasLeftContact = false;
            HasRightContact = false;
            _contactsCount = _collider.GetContacts(_contactPoints);
            for (int i = 0; i < _contactsCount; i++)
            {
                var normal = _contactPoints[i].normal;
                var rigidbody = _contactPoints[i].rigidbody;
                if (normal.y > _collisionTresh)
                {
                    IsGrounded = true;
                }

                if (normal.x > _collisionTresh && rigidbody == null)
                {
                    HasLeftContact = true;
                }

                if (normal.x < -_collisionTresh && rigidbody == null)
                {
                    HasRightContact = true;
                }
            }
        }

        #endregion
        
        
    }
}