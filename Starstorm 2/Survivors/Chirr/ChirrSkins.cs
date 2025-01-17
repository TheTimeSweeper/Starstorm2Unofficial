﻿using R2API;
using R2API.Utils;
using RoR2;
using Starstorm2Unofficial.Cores;
using Starstorm2Unofficial.Modules;
using System.Collections.Generic;
using UnityEngine;

namespace Starstorm2Unofficial.Survivors.Chirr
{
    public static class ChirrSkins
    {
        public static Mesh meshChirr;
        public static Mesh meshChirrMaid;
        public static Mesh meshChirrMaidDress;

        public static void LoadMeshes(AssetBundle assetBundle)
        {
            meshChirr = assetBundle.LoadAsset<Mesh>("meshChirr");
            meshChirrMaid = assetBundle.LoadAsset<Mesh>("meshChirrMaid");
            meshChirrMaidDress = assetBundle.LoadAsset<Mesh>("meshChirrMaidDress");
        }

        public static void RegisterSkins()
        {
            LoadMeshes(Modules.Assets.mainAssetBundle);

            GameObject bodyPrefab = ChirrCore.chirrPrefab;

            GameObject model = bodyPrefab.GetComponentInChildren<ModelLocator>().modelTransform.gameObject;
            CharacterModel characterModel = model.GetComponent<CharacterModel>();

            ModelSkinController skinController = model.AddComponent<ModelSkinController>();
            ChildLocator childLocator = model.GetComponent<ChildLocator>();

            SkinnedMeshRenderer mainRenderer = Reflection.GetFieldValue<SkinnedMeshRenderer>(characterModel, "mainSkinnedMeshRenderer");

            List<SkinDef> skinDefs = new List<SkinDef>();
            
            #region DefaultSkin

            //TODO: CHANGE TO meshChirr and matChirr. matChirr is set in ChirrCore for some reason.
            //meshChirr turns into a weird skeleton thingy

            CharacterModel.RendererInfo[] defaultRenderers = characterModel.baseRendererInfos;

            LanguageAPI.Add("CHIRR_DEFAULT_SKIN_NAME", "Default");
            SkinDef defaultSkin = SkinsCore.CreateSkinDef("CHIRR_DEFAULT_SKIN_NAME",
                                                          LoadoutAPI.CreateSkinIcon(new Color32(255, 255, 255, 255), new Color32(76, 116, 114, 255), new Color32(83, 118, 99, 255), new Color32(120, 147, 90, 255)),
                                                          defaultRenderers,
                                                          model,
                                                          null);

            defaultSkin.meshReplacements = SkinsCore.CreateMeshReplacements(defaultRenderers,
                                                                            meshChirr,
                                                                            null);

            defaultSkin.gameObjectActivations = new SkinDef.GameObjectActivation[] {
                new SkinDef.GameObjectActivation {
                    gameObject = childLocator.FindChildGameObject("ModelDress"),
                    shouldActivate = false,
                }
            };

            skinDefs.Add(defaultSkin);
            #endregion

            #region MaidSkin
            //Untested, disable this for now.
            if (true)
            {
                LanguageAPI.Add("ACHIEVEMENT_SS2UCHIRRGCLEARGAMEMONSOON_NAME", "Chirr: Mastery");
                LanguageAPI.Add("ACHIEVEMENT_SS2UCHIRRCLEARGAMEMONSOON_DESCRIPTION", "As Chirr, beat the game or obliterate on Monsoon.");

                /*UnlockableDef masterySkinUnlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
                masterySkinUnlockableDef.cachedName = "Skins.SS2UChirr.Mastery";
                masterySkinUnlockableDef.nameToken = "ACHIEVEMENT_SS2UCHIRRCLEARGAMEMONSOON_NAME";
                masterySkinUnlockableDef.achievementIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("cyborgsecondary");
                Unlockables.unlockableDefs.Add(masterySkinUnlockableDef);*/

                CharacterModel.RendererInfo[] masteryRendererInfos = new CharacterModel.RendererInfo[defaultRenderers.Length];
                defaultRenderers.CopyTo(masteryRendererInfos, 0);

                masteryRendererInfos[0].defaultMaterial = Modules.Assets.CreateMaterial("matChirr", 0, new Color(1f, 1f, 1f), 1);
                masteryRendererInfos[1].defaultMaterial = Modules.Assets.CreateMaterial("matChirrMaidDress", 0, new Color(1f, 1f, 1f), 0);

                LanguageAPI.Add("CHIRR_MASTERY_SKIN_NAME", "Maid");
                SkinDef masterySkin = SkinsCore.CreateSkinDef("CHIRR_MASTERY_SKIN_NAME",
                                                              LoadoutAPI.CreateSkinIcon(new Color32(234, 231, 212, 255), new Color32(125, 92, 39, 255), new Color32(26, 17, 22, 255), new Color32(57, 33, 33, 255)),
                                                              masteryRendererInfos,
                                                              model,
                                                              null);

                masterySkin.meshReplacements = SkinsCore.CreateMeshReplacements(masteryRendererInfos,
                                                                                meshChirrMaid,
                                                                                meshChirrMaidDress);

                masterySkin.gameObjectActivations = new SkinDef.GameObjectActivation[] {
                    new SkinDef.GameObjectActivation {
                        gameObject = childLocator.FindChildGameObject("ModelDress"),
                        shouldActivate = true,
                    }
                };

                skinDefs.Add(masterySkin);
            }
            #endregion

            skinController.skins = skinDefs.ToArray();
        }
    }
}
