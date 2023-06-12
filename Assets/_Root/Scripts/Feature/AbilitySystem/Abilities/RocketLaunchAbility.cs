using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Feature.AbilitySystem.Abilities
{
    internal class RocketLaunchAbility : IAbility
    {
        private readonly AbilityItemConfig _config;
        
        public RocketLaunchAbility([NotNull] AbilityItemConfig config) =>
            _config = config ? config : throw new ArgumentNullException(nameof(config));


        public void Apply(IAbilityActivator activator)
        {
            var projectile = Object.Instantiate(_config.Projectile).GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.right * _config.Value;
            projectile.AddForce(force, ForceMode2D.Impulse);
        }
    }
}