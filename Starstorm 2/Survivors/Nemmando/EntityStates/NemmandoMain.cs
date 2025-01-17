﻿using EntityStates;
using EntityStates.SS2UStates.Common;
using UnityEngine;

namespace EntityStates.SS2UStates.Nemmando
{
    public class NemmandoMain : BaseCustomMainState
    {
        private Animator animator;

        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();
        }

        public override void Update()
        {
            base.Update();

            if (base.isAuthority && base.characterMotor.isGrounded)
            {
                if (Input.GetKeyDown(Starstorm2Unofficial.Modules.Config.restKeybind))
                {
                    this.outer.SetInterruptState(new Common.Emotes.RestEmote(), InterruptPriority.Any);
                    return;
                }
                else if (Input.GetKeyDown(Starstorm2Unofficial.Modules.Config.tauntKeybind))
                {
                    this.outer.SetInterruptState(new Common.Emotes.TauntEmote(), InterruptPriority.Any);
                    return;
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this.animator)
            {
                if (base.hasSheath) this.animator.SetBool("isAttacking", !base.characterBody.outOfCombat);
                else this.animator.SetBool("isAttacking", true);
                //this.animator.SetBool("inCombat", (!base.characterBody.outOfCombat || !base.characterBody.outOfDanger));
            }
        }
    }
}