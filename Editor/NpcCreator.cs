using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NpcCreator : EditorWindow
{
    #region -- Variables for Npc Creator

    private enum Options
    {
        Gender,
        Head,
        Eyebrows,
        FacialHair,
        Helmet,
        NoHairHelmet,
        NoBeardHelmet,
        Hat,
        Torso,
        Arm_Upper_Right,
        Arm_Upper_Left,
        Arm_Lower_Right,
        Arm_Lower_Left,
        Hand_Right,
        Hand_Left,
        Hips,
        Leg_Right,
        Leg_Left,
        Ears,
        Hair,
        Head_Attachment,
        Back_Attachment,
        Shoulder_Right,
        Shoulder_Left,
        Elbow_Right,
        Elbow_Left,
        Hip_Attachment,
        Knee_Right,
        Knee_Left,
    }

    #region Option Lists

    List<GameObject> HeadList = new List<GameObject>();
    List<GameObject> EyebrowList = new List<GameObject>();
    List<GameObject> HairList = new List<GameObject>();
    List<GameObject> FacialHairList = new List<GameObject>();
    List<GameObject> EarsList = new List<GameObject>();
    List<GameObject> HelmetList = new List<GameObject>();
    List<GameObject> NoHairHelmetList = new List<GameObject>();
    List<GameObject> NoBeardHelmetList = new List<GameObject>();
    List<GameObject> HatList = new List<GameObject>();
    List<GameObject> TorsoList = new List<GameObject>();
    List<GameObject> HipsList = new List<GameObject>();
    List<GameObject> ArmUpperRightList = new List<GameObject>();
    List<GameObject> ArmUpperLeftList = new List<GameObject>();
    List<GameObject> ArmLowerRightList = new List<GameObject>();
    List<GameObject> ArmLowerLeftList = new List<GameObject>();
    List<GameObject> HandRightList = new List<GameObject>();
    List<GameObject> HandLeftList = new List<GameObject>();
    List<GameObject> LegRightList = new List<GameObject>();
    List<GameObject> LegLeftList = new List<GameObject>();
    List<GameObject> HeadAttachmentList = new List<GameObject>();
    List<GameObject> BackAttachmentList = new List<GameObject>();
    List<GameObject> HipAttachmentList = new List<GameObject>();
    List<GameObject> KneeRightList = new List<GameObject>();
    List<GameObject> KneeLeftList = new List<GameObject>();
    List<GameObject> ElbowRightList = new List<GameObject>();
    List<GameObject> ElbowLeftList = new List<GameObject>();
    List<GameObject> ShoulderRightList = new List<GameObject>();
    List<GameObject> ShoulderLeftList = new List<GameObject>();

    #endregion

    #region Option Indexes

    private static int GenderIndex;
    private static int HeadIndex;
    private static int EyebrowsIndex;
    private static int FacialHairIndex;
    private static int HelmetIndex;
    private static int NoHairHelmetIndex;
    private static int NoBeardHelmetIndex;
    private static int HatIndex;
    private static int TorsoIndex;
    private static int Arm_Upper_RightIndex;
    private static int Arm_Upper_LeftIndex;
    private static int Arm_Lower_RightIndex;
    private static int Arm_Lower_LeftIndex;
    private static int Hand_RightIndex;
    private static int Hand_LeftIndex;
    private static int HipsIndex;
    private static int Leg_RightIndex;
    private static int Leg_LeftIndex;
    private static int EarsIndex;
    private static int HairIndex;
    private static int Head_AttachmentIndex;
    private static int Back_AttachmentIndex;
    private static int Shoulder_RightIndex;
    private static int Shoulder_LeftIndex;
    private static int Elbow_RightIndex;
    private static int Elbow_LeftIndex;
    private static int Hip_AttachmentIndex;
    private static int Knee_RightIndex;
    private static int Knee_LeftIndex;

    #endregion

    #region Current Option Selections

    private GameObject currentGender;
    private GameObject currentHead;
    private GameObject currentEyebrows;
    private GameObject currentFacialHair;
    private GameObject currentHelmet;
    private GameObject currentNoHairHelmet;
    private GameObject currentNoBeardHelmet;
    private GameObject currentHat;
    private GameObject currentTorso;
    private GameObject currentArm_Upper_Right;
    private GameObject currentArm_Upper_Left;
    private GameObject currentArm_Lower_Right;
    private GameObject currentArm_Lower_Left;
    private GameObject currentHand_Right;
    private GameObject currentHand_Left;
    private GameObject currentHips;
    private GameObject currentLeg_Right;
    private GameObject currentLeg_Left;
    private GameObject currentEars;
    private GameObject currentHair;
    private GameObject currentHead_Attachment;
    private GameObject currentBack_Attachment;
    private GameObject currentShoulder_Right;
    private GameObject currentShoulder_Left;
    private GameObject currentElbow_Right;
    private GameObject currentElbow_Left;
    private GameObject currentHip_Attachment;
    private GameObject currentKnee_Right;
    private GameObject currentKnee_Left;

    #endregion

    SerializedProperty m_Helmet;

    private GameObject hierarchySelection = null;
    private Material hierarchySelectionMaterial;
    private GameObject MaleParts;
    private GameObject FemaleParts;
    private GameObject AllParts;

    #endregion

    #region -- Variables for Editor Window

    private enum WindowType
    {
        Start,
        Appearance,
        Armor,
        Attachments,
        Finalize
    }

    #region Current Slider Ints

    private int headint;
    private int eyebrowint;
    private int hairint;
    private int facialhairint;
    private int earint;
    private int helmetint;
    private int nohairhelmetint;
    private int nobeardhelmetint;
    private int hatint;
    private int torsoint;
    private int armupperrightint;
    private int armupperleftint;
    private int armlowerrightint;
    private int armlowerleftint;
    private int handleftint;
    private int handrightint;
    private int hipsint;
    private int legrightint;
    private int legleftint;
    private int shoulderrightint;
    private int shoulderleftint;
    private int elbowrightint;
    private int elbowleftint;
    private int kneerightint;
    private int kneeleftint;
    private int headattachmentint;
    private int backattachmentint;
    private int hipattachmentint;

    #endregion

    #region Tab Slider Values

    Vector2 AppearancescrollPos;
    Vector2 ArmorscrollPos;
    Vector2 AttachmentscrollPos;
    Vector2 FinalizescrollPos;

    #endregion

    private WindowType activeWindow;
    private bool IsSelectionLocked;
    private Texture2D unlockNPCimg;
    private Texture2D editorBanner;
    private Material loadedCharacterMaterial;
    private string npcname;

    private int placeholdheadint;
    private int placeholderhairint;
    private int placeholdereyebrowint;
    private int placeholderfacialhairint;

    #endregion

    [MenuItem("Tools/Synty Tools/NPC Creator")]
    public static void ShowWindow()
    {
        GetWindow<NpcCreator>("NPC Creator");
    }

    // Do this when the window is started
    private void OnEnable()
    {
        IsSelectionLocked = false;
        unlockNPCimg = Resources.Load("SYNTY_Lock") as Texture2D;
        editorBanner = Resources.Load("SYNTY_NpcCreator_Banner") as Texture2D;
    }

    //Do this when you draw the window
    private void OnGUI()
    {
        GUILayoutUtility.GetRect(1, 3, GUILayout.ExpandWidth(false));
        Rect space = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(editorBanner.height));
        float width = space.width;

        space.xMin = (width - editorBanner.width * 1.2f) / 2;
        if (space.xMin < 0) space.xMin = 0;

        space.width = editorBanner.width * 1.2f;
        space.height = editorBanner.height * 1.2f;
        GUI.DrawTexture(space, editorBanner, ScaleMode.ScaleToFit, true, 0);

        GUILayout.Space(15);

        DrawInterface();
    }

    private void DrawInterface()
    {
        if (hierarchySelection != null)
        {
            // This will occur if no valid npc is selected for editing
            if (IsSelectionLocked == false && hierarchySelection.activeInHierarchy)
            {
                EditorGUILayout.Space(5);
                loadedCharacterMaterial = (Material)EditorGUILayout.ObjectField("Load Material:",loadedCharacterMaterial, typeof(Material),true);
                if (GUILayout.Button("Customize NPC"))
                {
                    FemaleParts = hierarchySelection.transform.Find("Modular_Characters").Find("Female_Parts").gameObject;
                    MaleParts = hierarchySelection.transform.Find("Modular_Characters").Find("Male_Parts").gameObject;
                    AllParts = hierarchySelection.transform.Find("Modular_Characters").Find("All_Gender_Parts").gameObject;
                    
                    ClearCurrentOptionsObjs();
                    if(hierarchySelection.GetComponent<NpcCreatorSelector>().SavedGenderIndex == 0)
                    {
                        currentGender = MaleParts;
                    }
                    else
                    {
                        currentGender = FemaleParts;
                    }
                    GenerateOptionLists();

                    foreach (Transform child in FemaleParts.transform)
                    {
                        if (child.childCount <= 0)
                        {
                            child.gameObject.SetActive(false);
                        }
                        else
                        {
                            foreach (Transform child1 in child)
                            {
                                if (child1.childCount <= 0)
                                {
                                    child1.gameObject.SetActive(false);
                                }
                                else
                                {
                                    foreach (Transform child2 in child1)
                                        if (child2.childCount <= 0)
                                        {
                                            child2.gameObject.SetActive(false);
                                        }
                                        else
                                        {
                                        }
                                }

                            }
                        }
                    }

                    foreach (Transform child in MaleParts.transform)
                    {
                        if (child.childCount <= 0)
                        {
                            child.gameObject.SetActive(false);
                        }
                        else
                        {
                            foreach (Transform child1 in child)
                            {
                                if (child1.childCount <= 0)
                                {
                                    child1.gameObject.SetActive(false);
                                }
                                else
                                {
                                    foreach (Transform child2 in child1)
                                        if (child2.childCount <= 0)
                                        {
                                            child2.gameObject.SetActive(false);
                                        }
                                        else
                                        {
                                        }
                                }

                            }
                        }
                    }

                    foreach (Transform child in AllParts.transform)
                    {
                        if (child.childCount <= 0)
                        {
                            child.gameObject.SetActive(false);
                        }
                        else
                        {
                            foreach (Transform child1 in child)
                            {
                                if (child1.childCount <= 0)
                                {
                                    child1.gameObject.SetActive(false);
                                }
                                else
                                {
                                    foreach (Transform child2 in child1)
                                        if (child2.childCount <= 0)
                                        {
                                            child2.gameObject.SetActive(false);
                                        }
                                        else
                                        {
                                        }
                                }

                            }
                        }
                    }
                    
                    ApplyOptions(Options.Head, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHeadIndex);
                    placeholdheadint = HeadIndex;
                    ApplyOptions(Options.Eyebrows, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedEyebrowsIndex);
                    placeholdereyebrowint = EyebrowsIndex;
                    ApplyOptions(Options.Hair, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHairIndex);
                    placeholderhairint = HairIndex;
                    ApplyOptions(Options.Ears, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedEarsIndex);
                    ApplyOptions(Options.FacialHair, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedFacialHairIndex);
                    placeholderfacialhairint = FacialHairIndex;
                    ApplyOptions(Options.Helmet, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHelmetIndex);
                    ApplyOptions(Options.Torso, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedTorsoIndex);
                    ApplyOptions(Options.Hips, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHipsIndex);
                    ApplyOptions(Options.NoBeardHelmet, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedNoBeardHelmetIndex);
                    ApplyOptions(Options.NoHairHelmet, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedNoHairHelmetIndex);
                    ApplyOptions(Options.Hat, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHatIndex);
                    ApplyOptions(Options.Arm_Upper_Right, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedArm_Upper_RightIndex);
                    ApplyOptions(Options.Arm_Upper_Left, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedArm_Upper_LeftIndex);
                    ApplyOptions(Options.Arm_Lower_Right, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedArm_Lower_RightIndex);
                    ApplyOptions(Options.Arm_Lower_Left, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedArm_Lower_LeftIndex);
                    ApplyOptions(Options.Hand_Right, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHand_RightIndex);
                    ApplyOptions(Options.Hand_Left, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHand_LeftIndex);
                    ApplyOptions(Options.Leg_Left, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedLeg_LeftIndex);
                    ApplyOptions(Options.Leg_Right, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedLeg_RightIndex);
                    ApplyOptions(Options.Head_Attachment, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHead_AttachmentIndex);
                    ApplyOptions(Options.Hip_Attachment, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHip_AttachmentIndex);
                    ApplyOptions(Options.Back_Attachment, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedBack_AttachmentIndex);
                    ApplyOptions(Options.Shoulder_Right, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedShoulder_RightIndex);
                    ApplyOptions(Options.Shoulder_Left, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedShoulder_LeftIndex);
                    ApplyOptions(Options.Elbow_Right, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedElbow_RightIndex);
                    ApplyOptions(Options.Elbow_Left, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedElbow_LeftIndex);
                    ApplyOptions(Options.Knee_Right, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedKnee_RightIndex);
                    ApplyOptions(Options.Knee_Left, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedKnee_LeftIndex);
                    ApplyOptions(Options.Gender, hierarchySelection.GetComponent<NpcCreatorSelector>().SavedGenderIndex);
                    

                    ApplyMaterialToParts();
                    hierarchySelectionMaterial = AllParts.transform.Find("All_01_Hair").GetChild(1).GetComponent<Renderer>().sharedMaterial;

                    npcname = null;

                    Repaint();
                    activeWindow = WindowType.Start;
                    if (PrefabUtility.GetCorrespondingObjectFromSource(hierarchySelection) != null)
                    {
                        PrefabUtility.UnpackPrefabInstance(hierarchySelection, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
                    }
                    IsSelectionLocked = true;
                }
            }

            // This will occur if a valid npc is selected and locked in for editing
            if (IsSelectionLocked == true && hierarchySelection.activeInHierarchy)
            {
                DrawTabs();

                GUILayout.Space(5);
                
                switch (activeWindow)
                {
                    case WindowType.Start:
                        {
                            activeWindow = WindowType.Appearance;
                            break;
                        }

                    case WindowType.Appearance:
                        {
                            DrawApperanceGUI();
                            break;
                        }

                    case WindowType.Armor:
                        {
                            DrawArmorGUI();
                            break;
                        }

                    case WindowType.Attachments:
                        {
                            DrawAttachmentGUI();
                            break;
                        }

                    case WindowType.Finalize:
                        {
                            DrawFinalizeGUI();
                            break;
                        }
                }
            }
            else
            {
                IsSelectionLocked = false;
                
            }
        }
        else
        {
            EditorGUILayout.Space(5);
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.Label("No Npc selected");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }

    private void DrawTabs()
    {
        // The drawer for the button to unlock the npc thats locked in for editing, and clears the npc to a blank slate
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(unlockNPCimg, GUILayout.Height(25), GUILayout.Width(25)))
        {
            loadedCharacterMaterial = null;
            IsSelectionLocked = false;
        }
        GUILayout.Space(25);
        if (GUILayout.Button("Clear NPC Customization", GUILayout.Height(25)))
        {
            GenerateBlankCharacter();
        }
        GUILayout.Space(25);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        // The drawer for the npc customization tabs
        GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Appearance"))
        {
            activeWindow = WindowType.Appearance;
        }
        if (GUILayout.Button("Armor"))
        {
            activeWindow = WindowType.Armor;
        }
        if (GUILayout.Button("Attachments"))
        {
            activeWindow = WindowType.Attachments;
        }
        if (GUILayout.Button("Finalize"))
        {
            activeWindow = WindowType.Finalize;
        }
        GUILayout.EndHorizontal();
    }

   

    private void DrawApperanceGUI()
    {
        #region Appearance Panel

        AppearancescrollPos = EditorGUILayout.BeginScrollView(AppearancescrollPos, false, true, GUILayout.Width(position.width));
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
        GUILayout.Label("- Appearance -", EditorStyles.boldLabel);

        #region Gender Panel
        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Gender:", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Male"))
        {
            ApplyOptions(Options.Gender, 0);
            Repaint();
        }
        if (GUILayout.Button("Female"))
        {
            ApplyOptions(Options.Gender, 1);
            Repaint();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Face Panel
        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Face:", EditorStyles.boldLabel);
        headint = EditorGUILayout.IntSlider(HeadIndex, 1, HeadList.Count - 1);
        if (headint != HeadIndex)
        {
            HeadIndex = headint;
            ApplyOptions(Options.Head, HeadIndex);
        }
        GUILayout.EndVertical();
        #endregion


        GUILayout.Space(5);

        #region Eyebrow Panel
        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Eyebrow:", EditorStyles.boldLabel);
        eyebrowint = EditorGUILayout.IntSlider(EyebrowsIndex, 0, EyebrowList.Count - 1);
        if (eyebrowint != EyebrowsIndex)
        {
            EyebrowsIndex = eyebrowint;
            ApplyOptions(Options.Eyebrows, EyebrowsIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Hair Panel
        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Hair:", EditorStyles.boldLabel);
        hairint = EditorGUILayout.IntSlider(HairIndex, 0, HairList.Count - 1);
        if (hairint != HairIndex)
        {
            HairIndex = hairint;
            ApplyOptions(Options.Hair, HairIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Facial Hair Panel
        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Facial Hair:", EditorStyles.boldLabel);
        facialhairint = EditorGUILayout.IntSlider(FacialHairIndex, 0, FacialHairList.Count - 1);
        if (facialhairint != FacialHairIndex)
        {
            FacialHairIndex = facialhairint;
            ApplyOptions(Options.FacialHair, FacialHairIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Ear Panel
        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Ears:", EditorStyles.boldLabel);
        earint = EditorGUILayout.IntSlider(EarsIndex, 0, EarsList.Count - 1);
        if (earint != EarsIndex)
        {
            EarsIndex = earint;
            ApplyOptions(Options.Ears, EarsIndex);
        }
        GUILayout.EndVertical();
        #endregion
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(10);

        #region Colors Panel
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
        GUILayout.Label("- Appearance Colors -", EditorStyles.boldLabel);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Skin Color:",EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Skin", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Skin")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Eye Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Eyes", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Eyes")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Hair Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Hair", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Hair")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Stubble Color:", EditorStyles.boldLabel);
        GUILayout.Space(20);
        if (GUILayout.Button("None"))
        {
            hierarchySelectionMaterial.SetColor("_Color_Stubble", hierarchySelectionMaterial.GetColor("_Color_Skin"));
        }
        GUILayout.Space(10);
        GUILayout.EndHorizontal();
        hierarchySelectionMaterial.SetColor("_Color_Stubble", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Stubble")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Scar Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Scar", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Scar")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Body Art Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_BodyArt", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_BodyArt")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Body Art Amount:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetFloat("_BodyArt_Amount", EditorGUILayout.Slider(hierarchySelectionMaterial.GetFloat("_BodyArt_Amount"), 0, 1));
        GUILayout.EndVertical();
        GUILayout.EndVertical();
        #endregion

        EditorGUILayout.EndScrollView();
    }

    private void DrawArmorGUI()
    {
        ArmorscrollPos = EditorGUILayout.BeginScrollView(ArmorscrollPos, false, true, GUILayout.Width(position.width));
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
        GUILayout.Label("- Armor -", EditorStyles.boldLabel);

        GUILayout.Space(5);

        #region Helmet Panel
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Helmets", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Full Helmet:", EditorStyles.boldLabel);
        helmetint = EditorGUILayout.IntSlider(HelmetIndex, 0, HelmetList.Count - 1);
        if (helmetint != HelmetIndex)
        {
            HelmetIndex = helmetint;
            ApplyOptions(Options.Helmet, HelmetIndex);
        }

        GUILayout.Space(2);
        
        EditorGUILayout.LabelField("No Hair Helmets:", EditorStyles.boldLabel);
        nohairhelmetint = EditorGUILayout.IntSlider(NoHairHelmetIndex, 0, NoHairHelmetList.Count - 1);
        if (nohairhelmetint != NoHairHelmetIndex)
        {
            NoHairHelmetIndex = nohairhelmetint;
            ApplyOptions(Options.NoHairHelmet, NoHairHelmetIndex);
        }

        GUILayout.Space(2);
        
        EditorGUILayout.LabelField("No Beard Helmets:", EditorStyles.boldLabel);
        nobeardhelmetint = EditorGUILayout.IntSlider(NoBeardHelmetIndex, 0, NoBeardHelmetList.Count - 1);
        if (nobeardhelmetint != NoBeardHelmetIndex)
        {
            NoBeardHelmetIndex = nobeardhelmetint;
            ApplyOptions(Options.NoBeardHelmet, NoBeardHelmetIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Hats:", EditorStyles.boldLabel);
        hatint = EditorGUILayout.IntSlider(HatIndex, 0, HatList.Count - 1);
        if (hatint != HatIndex)
        {
            HatIndex = hatint;
            ApplyOptions(Options.Hat, HatIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Upper Body Panel
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Upper Body", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Torso:", EditorStyles.boldLabel);
        torsoint = EditorGUILayout.IntSlider(TorsoIndex, 0, TorsoList.Count - 1);
        if (torsoint != TorsoIndex)
        {
            TorsoIndex = torsoint;
            ApplyOptions(Options.Torso, TorsoIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Hips:", EditorStyles.boldLabel);
        hipsint = EditorGUILayout.IntSlider(HipsIndex, 0, HipsList.Count - 1);
        if (hipsint != HipsIndex)
        {
            HipsIndex = hipsint;
            ApplyOptions(Options.Hips, HipsIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Arm Panel
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Arms", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Upper Right Arm:", EditorStyles.boldLabel);
        armupperrightint = EditorGUILayout.IntSlider(Arm_Upper_RightIndex, 0, ArmUpperRightList.Count - 1);
        if (armupperrightint != Arm_Upper_RightIndex)
        {
            Arm_Upper_RightIndex = armupperrightint;
            ApplyOptions(Options.Arm_Upper_Right, Arm_Upper_RightIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Upper Left Arm:", EditorStyles.boldLabel);
        armupperleftint = EditorGUILayout.IntSlider(Arm_Upper_LeftIndex, 0, ArmUpperLeftList.Count - 1);
        if (armupperleftint != Arm_Upper_LeftIndex)
        {
            Arm_Upper_LeftIndex = armupperleftint;
            ApplyOptions(Options.Arm_Upper_Left, Arm_Upper_LeftIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Lower Left Arm:", EditorStyles.boldLabel);
        armlowerrightint = EditorGUILayout.IntSlider(Arm_Lower_RightIndex, 0, ArmLowerRightList.Count - 1);
        if (armlowerrightint != Arm_Lower_RightIndex)
        {
            Arm_Lower_RightIndex = armlowerrightint;
            ApplyOptions(Options.Arm_Lower_Right, Arm_Lower_RightIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Lower Left Arm:", EditorStyles.boldLabel);
        armlowerleftint = EditorGUILayout.IntSlider(Arm_Lower_LeftIndex, 0, ArmLowerLeftList.Count - 1);
        if (armlowerleftint != Arm_Lower_LeftIndex)
        {
            Arm_Lower_LeftIndex = armlowerleftint;
            ApplyOptions(Options.Arm_Lower_Left, Arm_Lower_LeftIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Right Hand:", EditorStyles.boldLabel);
        handrightint = EditorGUILayout.IntSlider(Hand_RightIndex, 0, HandRightList.Count - 1);
        if (handrightint != Hand_RightIndex)
        {
            Hand_RightIndex = handrightint;
            ApplyOptions(Options.Hand_Right, Hand_RightIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Left Hand:", EditorStyles.boldLabel);
        handleftint = EditorGUILayout.IntSlider(Hand_LeftIndex, 0, HandLeftList.Count - 1);
        if (handleftint != Hand_LeftIndex)
        {
            Hand_LeftIndex = handleftint;
            ApplyOptions(Options.Hand_Left, Hand_LeftIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Leg Panel
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Legs", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Right Leg:", EditorStyles.boldLabel);
        legrightint = EditorGUILayout.IntSlider(Leg_RightIndex, 0, LegRightList.Count - 1);
        if (legrightint != Leg_RightIndex)
        {
            Leg_RightIndex = legrightint;
            ApplyOptions(Options.Leg_Right, Leg_RightIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Left Leg:", EditorStyles.boldLabel);
        legleftint = EditorGUILayout.IntSlider(Leg_LeftIndex, 0, LegLeftList.Count - 1);
        if (legleftint != Leg_LeftIndex)
        {
            Leg_LeftIndex = legleftint;
            ApplyOptions(Options.Leg_Left, Leg_LeftIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.EndVertical();

        GUILayout.Space(5);

        DrawArmorColorPanel();
        EditorGUILayout.EndScrollView();
    }

    private void DrawAttachmentGUI()
    {
        AttachmentscrollPos = EditorGUILayout.BeginScrollView(AttachmentscrollPos, false, true, GUILayout.Width(position.width));
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
        GUILayout.Label("- Attachments -", EditorStyles.boldLabel);

        GUILayout.Space(5);

        #region Shoulders Panel
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Shoulders", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Left:", EditorStyles.boldLabel);
        shoulderleftint = EditorGUILayout.IntSlider(Shoulder_LeftIndex, 0, ShoulderLeftList.Count - 1);
        if (shoulderleftint != Shoulder_LeftIndex)
        {
            Shoulder_LeftIndex = shoulderleftint;
            ApplyOptions(Options.Shoulder_Left, Shoulder_LeftIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Right:", EditorStyles.boldLabel);
        shoulderrightint = EditorGUILayout.IntSlider(Shoulder_RightIndex, 0, ShoulderRightList.Count - 1);
        if (shoulderrightint != Shoulder_RightIndex)
        {
            Shoulder_RightIndex = shoulderrightint;
            ApplyOptions(Options.Shoulder_Right, Shoulder_RightIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Elbow Panel
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Elbow Guards", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Left:", EditorStyles.boldLabel);
        elbowleftint = EditorGUILayout.IntSlider(Elbow_LeftIndex, 0, ElbowLeftList.Count - 1);
        if (elbowleftint != Elbow_LeftIndex)
        {
            Elbow_LeftIndex = elbowleftint;
            ApplyOptions(Options.Elbow_Left, Elbow_LeftIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Right:", EditorStyles.boldLabel);
        elbowrightint = EditorGUILayout.IntSlider(Elbow_RightIndex, 0, ElbowRightList.Count - 1);
        if (elbowrightint != Elbow_RightIndex)
        {
            Elbow_RightIndex = elbowrightint;
            ApplyOptions(Options.Elbow_Right, Elbow_RightIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Knee Panel
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Knee Guards", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Left:", EditorStyles.boldLabel);
        kneeleftint = EditorGUILayout.IntSlider(Knee_LeftIndex, 0, KneeLeftList.Count - 1);
        if (kneeleftint != Knee_LeftIndex)
        {
            Knee_LeftIndex = kneeleftint;
            ApplyOptions(Options.Knee_Left, Knee_LeftIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Right:", EditorStyles.boldLabel);
        kneerightint = EditorGUILayout.IntSlider(Knee_RightIndex, 0, KneeRightList.Count - 1);
        if (kneerightint != Knee_RightIndex)
        {
            Knee_RightIndex = kneerightint;
            ApplyOptions(Options.Knee_Right, Knee_RightIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.Space(5);

        #region Extras Panel
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Extras", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Helmet Attachment:", EditorStyles.boldLabel);
        headattachmentint = EditorGUILayout.IntSlider(Head_AttachmentIndex, 0, HeadAttachmentList.Count - 1);
        if (headattachmentint != Head_AttachmentIndex)
        {
            Head_AttachmentIndex = headattachmentint;
            ApplyOptions(Options.Head_Attachment, Head_AttachmentIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Back Attachment:", EditorStyles.boldLabel);
        backattachmentint = EditorGUILayout.IntSlider(Back_AttachmentIndex, 0, BackAttachmentList.Count - 1);
        if (backattachmentint != Back_AttachmentIndex)
        {
            Back_AttachmentIndex = backattachmentint;
            ApplyOptions(Options.Back_Attachment, Back_AttachmentIndex);
        }

        GUILayout.Space(2);

        EditorGUILayout.LabelField("Hip Attachment:", EditorStyles.boldLabel);
        hipattachmentint = EditorGUILayout.IntSlider(Hip_AttachmentIndex, 0, HipAttachmentList.Count - 1);
        if (hipattachmentint != Hip_AttachmentIndex)
        {
            Hip_AttachmentIndex = hipattachmentint;
            ApplyOptions(Options.Hip_Attachment, Hip_AttachmentIndex);
        }
        GUILayout.EndVertical();
        #endregion

        GUILayout.EndVertical();

        GUILayout.Space(5);

        DrawArmorColorPanel();
        EditorGUILayout.EndScrollView();
    }

    private void DrawFinalizeGUI()
    {
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
        GUILayout.Label("- Finalize -", EditorStyles.boldLabel);
        npcname = EditorGUILayout.TextField("Npc Name:", npcname);

        GUILayout.Space(5);

        if (GUILayout.Button("Save Npc"))
        {
            hierarchySelection.name = npcname;
            if(GameObject.Find("NPCS"))
            {
                hierarchySelection.transform.SetParent(GameObject.Find("NPCS").transform);
            }
            else
            {
                var npcsGo = new GameObject("NPCS");
                hierarchySelection.transform.SetParent(npcsGo.transform);
            }
        }

        GUILayout.Space(5);

        if (GUILayout.Button("Save Npc As New Prefab"))
        {
            hierarchySelection.name = npcname;
            if (GameObject.Find("NPCS"))
            {
                hierarchySelection.transform.SetParent(GameObject.Find("NPCS").transform);
            }
            else
            {
                var npcsGo = new GameObject("NPCS");
                hierarchySelection.transform.SetParent(npcsGo.transform);
            }
            SavePrefab(npcname);
            IsSelectionLocked = false;
        }

        GUILayout.Space(5);

        if (GUILayout.Button("Save Npc As Overwrite"))
        {
            hierarchySelection.name = npcname;
            if (GameObject.Find("NPCS"))
            {
                hierarchySelection.transform.SetParent(GameObject.Find("NPCS").transform);
            }
            else
            {
                var npcsGo = new GameObject("NPCS");
                hierarchySelection.transform.SetParent(npcsGo.transform);
            }
            PrefabUtility.SaveAsPrefabAssetAndConnect(hierarchySelection, "Assets/ProjectUnderoo/Assets/Npcs/" + npcname + ".prefab", InteractionMode.AutomatedAction);
            IsSelectionLocked = false;
        }
        GUILayout.EndVertical();
    }

    private void SavePrefab(string name)
    {
        string savename = name;
        int variantint = 0;

        TryToSavePrefab:

        if(AssetDatabase.LoadAssetAtPath("Assets/ProjectUnderoo/Assets/Npcs/" + savename + ".prefab", typeof(GameObject)))
        {
            variantint++;
            savename = name + "0" + variantint.ToString();
            goto TryToSavePrefab;
        }
        else
        {
            PrefabUtility.SaveAsPrefabAssetAndConnect(hierarchySelection, "Assets/ProjectUnderoo/Assets/Npcs/" + savename + ".prefab", InteractionMode.AutomatedAction);
        }
    }

    private void DrawArmorColorPanel()
    {
        #region Colors Panel
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
        GUILayout.Label("- Armor Colors -", EditorStyles.boldLabel);

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Cloth Primary Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Primary", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Primary")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Cloth Accent Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Secondary", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Secondary")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Leather Primary Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Leather_Primary", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Leather_Primary")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Leather Accent Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Leather_Secondary", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Leather_Secondary")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Metal Primary Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Metal_Primary", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Metal_Primary")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Metal Accent Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Metal_Secondary", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Metal_Secondary")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Metal Dark Color:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetColor("_Color_Metal_Dark", EditorGUILayout.ColorField(hierarchySelectionMaterial.GetColor("_Color_Metal_Dark")));
        GUILayout.EndVertical();

        GUILayout.Space(5);

        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Metallic:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetFloat("_Metallic", EditorGUILayout.Slider(hierarchySelectionMaterial.GetFloat("_Metallic"), 0, 1));

        GUILayout.Space(5);
        
        EditorGUILayout.LabelField("Smoothness:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetFloat("_Smoothness", EditorGUILayout.Slider(hierarchySelectionMaterial.GetFloat("_Smoothness"), 0, 1));

        GUILayout.Space(5);
        
        EditorGUILayout.LabelField("Emission:", EditorStyles.boldLabel);
        hierarchySelectionMaterial.SetFloat("_Emission", EditorGUILayout.Slider(hierarchySelectionMaterial.GetFloat("_Emission"), 0, 1));
        GUILayout.EndVertical();
        GUILayout.EndVertical();
        #endregion
    }

    private void ApplyMaterialToParts()
    {
        if(loadedCharacterMaterial != null)
        {
            foreach (Transform child in FemaleParts.transform)
            {
                if (child.GetComponent<Renderer>())
                {
                    child.gameObject.GetComponent<Renderer>().sharedMaterial = loadedCharacterMaterial;
                }
                else
                {
                    foreach (Transform child1 in child)
                    {
                        if (child1.GetComponent<Renderer>())
                        {
                            child1.gameObject.GetComponent<Renderer>().sharedMaterial = loadedCharacterMaterial;
                        }
                        else
                        {
                            foreach (Transform child2 in child1)
                                if (child2.GetComponent<Renderer>())
                                {
                                    child2.gameObject.GetComponent<Renderer>().sharedMaterial = loadedCharacterMaterial;
                                }
                                else
                                {
                                }
                        }

                    }
                }
            }

            foreach (Transform child in MaleParts.transform)
            {
                if (child.GetComponent<Renderer>())
                {
                    child.gameObject.GetComponent<Renderer>().sharedMaterial = loadedCharacterMaterial;
                }
                else
                {
                    foreach (Transform child1 in child)
                    {
                        if (child1.GetComponent<Renderer>())
                        {
                            child1.gameObject.GetComponent<Renderer>().sharedMaterial = loadedCharacterMaterial;
                        }
                        else
                        {
                            foreach (Transform child2 in child1)
                                if (child2.GetComponent<Renderer>())
                                {
                                    child2.gameObject.GetComponent<Renderer>().sharedMaterial = loadedCharacterMaterial;
                                }
                                else
                                {
                                }
                        }

                    }
                }
            }

            foreach (Transform child in AllParts.transform)
                    {
                        if (child.GetComponent<Renderer>())
                        {
                            child.gameObject.GetComponent<Renderer>().sharedMaterial = loadedCharacterMaterial;
                        }
                        else
                        {
                            foreach (Transform child1 in child)
                            {
                                if (child1.GetComponent<Renderer>())
                                {
                                    child1.gameObject.GetComponent<Renderer>().sharedMaterial = loadedCharacterMaterial;
                                }
                                else
                                {
                                    foreach (Transform child2 in child1)
                                        if (child2.GetComponent<Renderer>())
                                        {
                                            child2.gameObject.GetComponent<Renderer>().sharedMaterial = loadedCharacterMaterial;
                                        }
                                        else
                                        {
                                        }
                                }

                            }
                        }
                    }
        }
    }

    // Check for currently selected object in Heirarchy, then update NPC creator GUI based on currently selected object
    private void OnSelectionChange()
    {
        if (Selection.activeGameObject && IsSelectionLocked == false)
        {
            if (Selection.activeGameObject.GetComponent<NpcCreatorSelector>())
            {
                hierarchySelection = Selection.activeGameObject;
                Repaint();
            }
            else
                hierarchySelection = null;
                loadedCharacterMaterial = null;
                Repaint();
        }
        else if (!Selection.activeGameObject && IsSelectionLocked == false)
            hierarchySelection = null;
            loadedCharacterMaterial = null;
            Repaint();
    }

    private void ClearCurrentOptionsObjs()
    {
        currentHead = null;
        currentEyebrows = null;
        currentHair = null;
        currentFacialHair = null;
        currentEars = null;
        currentHelmet = null;
        currentNoBeardHelmet = null;
        currentNoHairHelmet = null;
        currentHat = null;
        currentTorso = null;
        currentHips = null;
        currentArm_Upper_Right = null;
        currentArm_Upper_Left = null;
        currentArm_Lower_Right = null;
        currentArm_Lower_Left = null;
        currentHand_Right = null;
        currentHand_Left = null;
        currentLeg_Right = null;
        currentLeg_Left = null;
        currentShoulder_Left = null;
        currentShoulder_Right = null;
        currentHead_Attachment = null;
        currentBack_Attachment = null;
        currentHip_Attachment = null;
        currentElbow_Left = null;
        currentElbow_Right = null;
        currentKnee_Left = null;
        currentKnee_Right = null;
    }

    private void GenerateBlankCharacter()
    {
        foreach(Transform child in FemaleParts.transform)
        {
            if(child.childCount <= 0)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                foreach(Transform child1 in child)
                {
                    if (child1.childCount <= 0)
                    {
                        child1.gameObject.SetActive(false);
                    }
                    else
                    {
                        foreach (Transform child2 in child1)
                            if (child2.childCount <= 0)
                            {
                                child2.gameObject.SetActive(false);
                            }
                            else
                            {
                            }
                    }

                }
            }
        }

        foreach (Transform child in MaleParts.transform)
        {
            if (child.childCount <= 0)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                foreach (Transform child1 in child)
                {
                    if (child1.childCount <= 0)
                    {
                        child1.gameObject.SetActive(false);
                    }
                    else
                    {
                        foreach (Transform child2 in child1)
                            if (child2.childCount <= 0)
                            {
                                child2.gameObject.SetActive(false);
                            }
                            else
                            {
                            }
                    }

                }
            }
        }

        foreach (Transform child in AllParts.transform)
        {
            if (child.childCount <= 0)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                foreach (Transform child1 in child)
                {
                    if (child1.childCount <= 0)
                    {
                        child1.gameObject.SetActive(false);
                    }
                    else
                    {
                        foreach (Transform child2 in child1)
                            if (child2.childCount <= 0)
                            {
                                child2.gameObject.SetActive(false);
                            }
                            else
                            {
                            }
                    }

                }
            }
        }

        ApplyOptions(Options.Gender, GenderIndex);
        ApplyOptions(Options.Head, 1);
        placeholdheadint = HeadIndex;
        ApplyOptions(Options.Eyebrows, 0);
        placeholdereyebrowint = EyebrowsIndex;
        ApplyOptions(Options.Hair, 0);
        placeholderhairint = HairIndex;
        ApplyOptions(Options.FacialHair, 0);
        placeholderfacialhairint = FacialHairIndex;
        ApplyOptions(Options.Ears, 0);
        ApplyOptions(Options.Torso, 0);
        ApplyOptions(Options.Hips, 0);
        ApplyOptions(Options.Arm_Upper_Right, 0);
        ApplyOptions(Options.Arm_Upper_Left, 0);
        ApplyOptions(Options.Arm_Lower_Right, 0);
        ApplyOptions(Options.Arm_Lower_Left, 0);
        ApplyOptions(Options.Hand_Right, 0);
        ApplyOptions(Options.Hand_Left, 0);
        ApplyOptions(Options.Leg_Right, 0);
        ApplyOptions(Options.Leg_Left, 0);
        ApplyOptions(Options.Head_Attachment, 0);
        ApplyOptions(Options.Hip_Attachment, 0);
        ApplyOptions(Options.Back_Attachment, 0);
        ApplyOptions(Options.Elbow_Left, 0);
        ApplyOptions(Options.Elbow_Right, 0);
        ApplyOptions(Options.Knee_Left, 0);
        ApplyOptions(Options.Knee_Right, 0);
        ApplyOptions(Options.Shoulder_Left, 0);
        ApplyOptions(Options.Shoulder_Right, 0);
    }

    private void GenerateOptionLists()
    {
        #region Clear Lists
        HeadList.Clear();
        EyebrowList.Clear();
        HairList.Clear();
        FacialHairList.Clear();
        HelmetList.Clear();
        NoHairHelmetList.Clear();
        NoBeardHelmetList.Clear();
        HatList.Clear();
        TorsoList.Clear();
        EarsList.Clear();
        HipsList.Clear();
        ArmUpperRightList.Clear();
        ArmUpperLeftList.Clear();
        ArmLowerRightList.Clear();
        ArmLowerLeftList.Clear();
        HandLeftList.Clear();
        HandRightList.Clear();
        LegLeftList.Clear();
        LegRightList.Clear();
        HeadAttachmentList.Clear();
        BackAttachmentList.Clear();
        HipAttachmentList.Clear();
        ElbowRightList.Clear();
        ElbowLeftList.Clear();
        KneeRightList.Clear();
        KneeLeftList.Clear();
        ShoulderLeftList.Clear();
        ShoulderRightList.Clear();
        #endregion

        foreach (Transform child in currentGender.transform.GetChild(0).GetChild(0))
        {
            HeadList.Add(child.gameObject);
        }
        
        foreach(Transform child in currentGender.transform.GetChild(1))
        {
            EyebrowList.Add(child.gameObject);
        }
        
        foreach (Transform child in AllParts.transform.GetChild(1))
        {
            HairList.Add(child.gameObject);
        }
        
        foreach (Transform child in currentGender.transform.GetChild(2))
        {
            FacialHairList.Add(child.gameObject);
        }
        
        foreach (Transform child in AllParts.transform.GetChild(12).GetChild(0))
        {
            EarsList.Add(child.gameObject);
        }
        
        foreach (Transform child in currentGender.transform.GetChild(0).GetChild(1))
        {
            HelmetList.Add(child.gameObject);
        }
        
        foreach (Transform child in AllParts.transform.GetChild(0).GetChild(0))
        {
            HatList.Add(child.gameObject);
        }
        
        foreach (Transform child in AllParts.transform.GetChild(0).GetChild(1))
        {
            NoBeardHelmetList.Add(child.gameObject);
        }
        
        foreach (Transform child in AllParts.transform.GetChild(0).GetChild(2))
        {
            NoHairHelmetList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(3))
        {
            TorsoList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(10))
        {
            HipsList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(4))
        {
            ArmUpperRightList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(5))
        {
            ArmUpperLeftList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(6))
        {
            ArmLowerRightList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(7))
        {
            ArmLowerLeftList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(8))
        {
            HandRightList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(9))
        {
            HandLeftList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(11))
        {
            LegRightList.Add(child.gameObject);
        }

        foreach (Transform child in currentGender.transform.GetChild(12))
        {
            LegLeftList.Add(child.gameObject);
        }

        foreach (Transform child in AllParts.transform.GetChild(2).GetChild(1))
        {
            HeadAttachmentList.Add(child.gameObject);
        }

        foreach (Transform child in AllParts.transform.GetChild(4))
        {
            BackAttachmentList.Add(child.gameObject);
        }

        foreach (Transform child in AllParts.transform.GetChild(5))
        {
            ShoulderRightList.Add(child.gameObject);
        }

        foreach (Transform child in AllParts.transform.GetChild(6))
        {
            ShoulderLeftList.Add(child.gameObject);
        }

        foreach (Transform child in AllParts.transform.GetChild(7))
        {
            ElbowRightList.Add(child.gameObject);
        }

        foreach (Transform child in AllParts.transform.GetChild(8))
        {
            ElbowLeftList.Add(child.gameObject);
        }

        foreach (Transform child in AllParts.transform.GetChild(9))
        {
            HipAttachmentList.Add(child.gameObject);
        }

        foreach (Transform child in AllParts.transform.GetChild(10))
        {
            KneeRightList.Add(child.gameObject);
        }

        foreach (Transform child in AllParts.transform.GetChild(11))
        {
            KneeLeftList.Add(child.gameObject);
        }
    }

    void ApplyOptions(Options type, int id)
    {
        switch(type)
        {
            case Options.Gender:
                {
                    {
                        GenderIndex = id;
                        if (id == 0)
                        {
                            currentGender = MaleParts;
                        }
                        else if (id == 1)
                        {
                            currentGender = FemaleParts;
                        }
                    }
                    GenerateOptionLists();

                    ApplyOptions(Options.Head, HeadIndex);
                    placeholdheadint = HeadIndex;
                    ApplyOptions(Options.Eyebrows, EyebrowsIndex);
                    placeholdereyebrowint = EyebrowsIndex;
                    ApplyOptions(Options.FacialHair, FacialHairIndex);
                    placeholderfacialhairint = FacialHairIndex;
                    placeholderhairint = HairIndex;
                    ApplyOptions(Options.Helmet, HelmetIndex);
                    ApplyOptions(Options.Torso, TorsoIndex);
                    ApplyOptions(Options.Hips, HipsIndex);
                    ApplyOptions(Options.Arm_Upper_Right, Arm_Upper_RightIndex);
                    ApplyOptions(Options.Arm_Upper_Left, Arm_Upper_LeftIndex);
                    ApplyOptions(Options.Arm_Lower_Right, Arm_Lower_RightIndex);
                    ApplyOptions(Options.Arm_Lower_Left, Arm_Lower_LeftIndex);
                    ApplyOptions(Options.Hand_Right, Hand_RightIndex);
                    ApplyOptions(Options.Hand_Left, Hand_LeftIndex);
                    ApplyOptions(Options.Leg_Left, Leg_LeftIndex);
                    ApplyOptions(Options.Leg_Right, Leg_RightIndex);
                    ApplyOptions(Options.Head_Attachment, Head_AttachmentIndex);
                    ApplyOptions(Options.Hip_Attachment, Hip_AttachmentIndex);
                    ApplyOptions(Options.Back_Attachment, Back_AttachmentIndex);
                    ApplyOptions(Options.Shoulder_Right, Shoulder_RightIndex);
                    ApplyOptions(Options.Shoulder_Left, Shoulder_LeftIndex);
                    ApplyOptions(Options.Elbow_Right, Elbow_RightIndex);
                    ApplyOptions(Options.Elbow_Left, Elbow_LeftIndex);
                    ApplyOptions(Options.Knee_Right, Knee_RightIndex);
                    ApplyOptions(Options.Knee_Left, Knee_LeftIndex);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedGenderIndex = id;
                    break;
                }

            case Options.Head:
                {
                    if (currentHead != null)
                    {
                        currentHead.SetActive(false);
                    }

                    HeadIndex = id;
                    if (HeadIndex > HeadList.Count - 1)
                    {
                        HeadIndex = HeadList.Count - 1;
                    }

                    headint = HeadIndex;
                    currentHead = HeadList[HeadIndex];
                    currentHead.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHeadIndex = id;
                    break;
                }

            case Options.Eyebrows:
                {
                    if (currentEyebrows != null)
                    {
                        currentEyebrows.SetActive(false);
                    }

                    EyebrowsIndex = id;
                    if (EyebrowsIndex > EyebrowList.Count - 1)
                    {
                        EyebrowsIndex = EyebrowList.Count - 1;
                    }

                    eyebrowint = EyebrowsIndex;
                    currentEyebrows = EyebrowList[EyebrowsIndex];
                    currentEyebrows.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedEyebrowsIndex = id;
                    break;
                }

            case Options.Hair:
                {
                    if (currentHair != null)
                    {
                        currentHair.SetActive(false);
                    }

                    HairIndex = id;
                    hairint = HairIndex;
                    currentHair = HairList[HairIndex];
                    currentHair.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHairIndex = id;
                    break;
                }

            case Options.FacialHair:
                {
                    if (currentFacialHair != null)
                    {
                        currentFacialHair.SetActive(false);
                    }

                    FacialHairIndex = id;
                    if (FacialHairIndex > FacialHairList.Count - 1)
                    {
                        FacialHairIndex = FacialHairList.Count - 1;
                        if(FacialHairList.Count == 0)
                        {
                            FacialHairIndex = 0;
                        }
                    }

                    facialhairint = FacialHairIndex;
                    currentFacialHair = FacialHairList[FacialHairIndex];
                    currentFacialHair.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedFacialHairIndex = id;
                    break;
                }

            case Options.Ears:
                {
                    if (currentEars != null)
                    {
                        currentEars.SetActive(false);
                    }

                    EarsIndex = id;
                    if (EarsIndex > EarsList.Count - 1)
                    {
                        EarsIndex = EarsList.Count - 1;
                    }

                    earint = EarsIndex;
                    currentEars = EarsList[EarsIndex];
                    currentEars.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedEarsIndex = id;
                    break;
                }

            case Options.Helmet:
                {
                    if (currentHelmet != null)
                    {
                        currentHelmet.SetActive(false);
                    }

                    HelmetIndex = id;

                    if (HelmetIndex != 0)
                    {
                        if (HeadIndex != 0)
                        {
                            placeholdheadint = HeadIndex;
                        }
                        if (EyebrowsIndex != 0)
                        {
                            placeholdereyebrowint = EyebrowsIndex;
                        }
                        if (HairIndex != 0)
                        {
                            placeholderhairint = HairIndex;
                        }
                        if (FacialHairIndex != 0)
                        {
                            placeholderfacialhairint = FacialHairIndex;
                        }

                        ApplyOptions(Options.Head, 0);
                        ApplyOptions(Options.Hair, 0);
                        ApplyOptions(Options.Eyebrows, 0);
                        ApplyOptions(Options.FacialHair, 0);
                    }
                    else
                    {
                        ApplyOptions(Options.Head, placeholdheadint);
                        ApplyOptions(Options.Eyebrows, placeholdereyebrowint);
                        ApplyOptions(Options.FacialHair, placeholderfacialhairint);
                        ApplyOptions(Options.Hair, placeholderhairint);
                    }

                    currentHelmet = HelmetList[HelmetIndex];
                    currentHelmet.SetActive(true);
                    
                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHelmetIndex = id;
                    Undo.RecordObject(hierarchySelection.GetComponent<NpcCreatorSelector>(), "");
                    break;
                }

            case Options.NoHairHelmet:
                {
                    if (currentNoHairHelmet != null)
                    {
                        currentNoHairHelmet.SetActive(false);
                    }

                    NoHairHelmetIndex = id;

                    if (NoHairHelmetIndex != 0)
                    {
                        if (HairIndex != 0)
                        {
                            placeholderhairint = HairIndex;
                        }
                        ApplyOptions(Options.Hair, 0);
                        if(HelmetIndex != 0)
                        {
                            if (HeadIndex != 0)
                            {
                                placeholdheadint = HeadIndex;
                            }
                            if (EyebrowsIndex != 0)
                            {
                                placeholdereyebrowint = EyebrowsIndex;
                            }
                            if (FacialHairIndex != 0)
                            {
                                placeholderfacialhairint = FacialHairIndex;
                            }

                            ApplyOptions(Options.Head, 0);
                            ApplyOptions(Options.Eyebrows, 0);
                            ApplyOptions(Options.FacialHair, 0);
                        }
                    }
                    else
                    {
                        if (HelmetIndex == 0)
                        { 
                            ApplyOptions(Options.Hair, placeholderhairint);
                            ApplyOptions(Options.Head, placeholdheadint);
                            ApplyOptions(Options.Eyebrows, placeholdereyebrowint);
                            ApplyOptions(Options.FacialHair, placeholderfacialhairint);
                        }
                    }

                    currentNoHairHelmet = NoHairHelmetList[NoHairHelmetIndex];
                    currentNoHairHelmet.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedNoHairHelmetIndex = id;
                    break;
                }

            case Options.NoBeardHelmet:
                {
                    if (currentNoBeardHelmet != null)
                    {
                        currentNoBeardHelmet.SetActive(false);
                    }

                    NoBeardHelmetIndex = id;

                    if (NoBeardHelmetIndex != 0)
                    {
                        if (FacialHairIndex != 0)
                        {
                            placeholderfacialhairint = FacialHairIndex;
                        }
                        ApplyOptions(Options.FacialHair, 0);
                        if (HelmetIndex != 0)
                        {
                            if (HeadIndex != 0)
                            {
                                placeholdheadint = HeadIndex;
                            }
                            if (EyebrowsIndex != 0)
                            {
                                placeholdereyebrowint = EyebrowsIndex;
                            }
                            if (HairIndex != 0)
                            {
                                placeholderhairint = HairIndex;
                            }

                            ApplyOptions(Options.Head, 0);
                            ApplyOptions(Options.Eyebrows, 0);
                            ApplyOptions(Options.Hair, 0);
                        }
                    }
                    else
                    {
                        if (HelmetIndex == 0)
                        {
                            ApplyOptions(Options.Hair, placeholderhairint);
                            ApplyOptions(Options.Head, placeholdheadint);
                            ApplyOptions(Options.Eyebrows, placeholdereyebrowint);
                            ApplyOptions(Options.FacialHair, placeholderfacialhairint);
                        }
                    }

                    currentNoBeardHelmet = NoBeardHelmetList[NoBeardHelmetIndex];
                    currentNoBeardHelmet.SetActive(true);
                    
                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedNoBeardHelmetIndex = id;
                    break;
                }

            case Options.Hat:
                {
                    if (currentHat != null)
                    {
                        currentHat.SetActive(false);
                    }

                    HatIndex = id;

                    currentHat = HatList[HatIndex];
                    currentHat.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHatIndex = id;
                    break;
                }

            case Options.Torso:
                {
                    if (currentTorso != null)
                    {
                        currentTorso.SetActive(false);
                    }

                    TorsoIndex = id;
                    if (TorsoIndex > TorsoList.Count - 1)
                    {
                        torsoint = TorsoList.Count - 1;
                        TorsoIndex = torsoint;
                    }

                    currentTorso = TorsoList[TorsoIndex];
                    currentTorso.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedTorsoIndex = id;
                    break;
                }

            case Options.Shoulder_Right:
                {
                    if (currentShoulder_Right != null)
                    {
                        currentShoulder_Right.SetActive(false);
                    }

                    Shoulder_RightIndex = id;
                    if (Shoulder_RightIndex > ShoulderRightList.Count - 1)
                    {
                        shoulderrightint = ShoulderRightList.Count - 1;
                        Shoulder_RightIndex = shoulderrightint;
                    }

                    currentShoulder_Right = ShoulderRightList[Shoulder_RightIndex];
                    currentShoulder_Right.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedShoulder_RightIndex = id;
                    break;
                }

            case Options.Shoulder_Left:
                {
                    if (currentShoulder_Left != null)
                    {
                        currentShoulder_Left.SetActive(false);
                    }

                    Shoulder_LeftIndex = id;
                    if (Shoulder_LeftIndex > ShoulderLeftList.Count - 1)
                    {
                        shoulderleftint = ShoulderLeftList.Count - 1;
                        Shoulder_LeftIndex = shoulderleftint;
                    }

                    currentShoulder_Left = ShoulderLeftList[Shoulder_LeftIndex];
                    currentShoulder_Left.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedShoulder_LeftIndex = id;
                    break;
                }

            case Options.Arm_Upper_Right:
                {
                    if (currentArm_Upper_Right != null)
                    {
                        currentArm_Upper_Right.SetActive(false);
                    }

                    Arm_Upper_RightIndex = id;
                    if (Arm_Upper_RightIndex > ArmUpperRightList.Count - 1)
                    {
                        armupperrightint = ArmUpperRightList.Count - 1;
                        Arm_Upper_RightIndex = armupperrightint;
                    }

                    currentArm_Upper_Right = ArmUpperRightList[Arm_Upper_RightIndex];
                    currentArm_Upper_Right.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedArm_Upper_RightIndex = id;
                    break;
                }

            case Options.Arm_Upper_Left:
                {
                    if (currentArm_Upper_Left != null)
                    {
                        currentArm_Upper_Left.SetActive(false);
                    }

                    Arm_Upper_LeftIndex = id;
                    if (Arm_Upper_LeftIndex > ArmUpperLeftList.Count - 1)
                    {
                        armupperleftint = ArmUpperLeftList.Count - 1;
                        Arm_Upper_LeftIndex = armupperleftint;
                    }

                    currentArm_Upper_Left = ArmUpperLeftList[Arm_Upper_LeftIndex];
                    currentArm_Upper_Left.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedArm_Upper_LeftIndex = id;
                    break;
                }

            case Options.Elbow_Right:
                {
                    if (currentElbow_Right != null)
                    {
                        currentElbow_Right.SetActive(false);
                    }

                    Elbow_RightIndex = id;
                    if (Elbow_RightIndex > ElbowRightList.Count - 1)
                    {
                        elbowrightint = ElbowRightList.Count - 1;
                        Elbow_RightIndex = elbowrightint;
                    }

                    currentElbow_Right = ElbowRightList[Elbow_RightIndex];
                    currentElbow_Right.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedElbow_RightIndex = id;
                    break;
                }

            case Options.Elbow_Left:
                {
                    if (currentElbow_Left != null)
                    {
                        currentElbow_Left.SetActive(false);
                    }

                    Elbow_LeftIndex = id;
                    if (Elbow_LeftIndex > ElbowLeftList.Count - 1)
                    {
                        elbowleftint = ElbowLeftList.Count - 1;
                        Elbow_LeftIndex = elbowleftint;
                    }

                    currentElbow_Left = ElbowLeftList[Elbow_LeftIndex];
                    currentElbow_Left.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedElbow_LeftIndex = id;
                    break;
                }

            case Options.Arm_Lower_Right:
                {
                    if (currentArm_Lower_Right != null)
                    {
                        currentArm_Lower_Right.SetActive(false);
                    }

                    Arm_Lower_RightIndex = id;
                    if (Arm_Lower_RightIndex > ArmLowerRightList.Count - 1)
                    {
                        armlowerrightint = ArmLowerRightList.Count - 1;
                        Arm_Lower_RightIndex = armlowerrightint;
                    }

                    currentArm_Lower_Right = ArmLowerRightList[Arm_Lower_RightIndex];
                    currentArm_Lower_Right.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedArm_Lower_RightIndex = id;
                    break;
                }

            case Options.Arm_Lower_Left:
                {
                    if (currentArm_Lower_Left != null)
                    {
                        currentArm_Lower_Left.SetActive(false);
                    }

                    Arm_Lower_LeftIndex = id;
                    if (Arm_Lower_LeftIndex > ArmLowerLeftList.Count - 1)
                    {
                        armlowerleftint = ArmLowerLeftList.Count - 1;
                        Arm_Lower_LeftIndex = armlowerleftint;
                    }

                    currentArm_Lower_Left = ArmLowerLeftList[Arm_Lower_LeftIndex];
                    currentArm_Lower_Left.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedArm_Lower_LeftIndex = id;
                    break;
                }

            case Options.Hand_Right:
                {
                    if (currentHand_Right != null)
                    {
                        currentHand_Right.SetActive(false);
                    }

                    Hand_RightIndex = id;
                    if (Hand_RightIndex > HandRightList.Count - 1)
                    {
                        handrightint = HandRightList.Count - 1;
                        Hand_RightIndex = handrightint;
                    }

                    currentHand_Right = HandRightList[Hand_RightIndex];
                    currentHand_Right.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHand_RightIndex = id;
                    break;
                }

            case Options.Hand_Left:
                {
                    if (currentHand_Left != null)
                    {
                        currentHand_Left.SetActive(false);
                    }

                    Hand_LeftIndex = id;
                    if (Hand_LeftIndex > HandLeftList.Count - 1)
                    {
                        handleftint = HandLeftList.Count - 1;
                        Hand_LeftIndex = handleftint;
                    }

                    currentHand_Left = HandLeftList[Hand_LeftIndex];
                    currentHand_Left.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHand_LeftIndex = id;
                    break;
                }

            case Options.Hips:
                {
                    if (currentHips != null)
                    {
                        currentHips.SetActive(false);
                    }

                    HipsIndex = id;
                    if (HipsIndex > HipsList.Count - 1)
                    {
                        hipsint = HipsList.Count - 1;
                        HipsIndex = hipsint;
                    }

                    currentHips = HipsList[HipsIndex];
                    currentHips.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHipsIndex = id;
                    break;
                }

            case Options.Knee_Right:
                {
                    if (currentKnee_Right != null)
                    {
                        currentKnee_Right.SetActive(false);
                    }

                    Knee_RightIndex = id;
                    if (Knee_RightIndex > KneeRightList.Count - 1)
                    {
                        kneerightint = KneeRightList.Count - 1;
                        Knee_RightIndex = kneerightint;
                    }

                    currentKnee_Right = KneeRightList[Knee_RightIndex];
                    currentKnee_Right.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedKnee_RightIndex = id;
                    break;
                }

            case Options.Knee_Left:
                {
                    if (currentKnee_Left != null)
                    {
                        currentKnee_Left.SetActive(false);
                    }

                    Knee_LeftIndex = id;
                    if (Knee_LeftIndex > KneeLeftList.Count - 1)
                    {
                        kneeleftint = KneeLeftList.Count - 1;
                        Knee_LeftIndex = kneeleftint;
                    }

                    currentKnee_Left = KneeLeftList[Knee_LeftIndex];
                    currentKnee_Left.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedKnee_LeftIndex = id;
                    break;
                }

            case Options.Leg_Right:
                {
                    if (currentLeg_Right != null)
                    {
                        currentLeg_Right.SetActive(false);
                    }

                    Leg_RightIndex = id;
                    if (Leg_RightIndex > LegRightList.Count - 1)
                    {
                        legrightint = LegRightList.Count - 1;
                        Leg_RightIndex = legrightint;
                    }

                    currentLeg_Right = LegRightList[Leg_RightIndex];
                    currentLeg_Right.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedLeg_RightIndex = id;
                    break;
                }

            case Options.Leg_Left:
                {
                    if (currentLeg_Left != null)
                    {
                        currentLeg_Left.SetActive(false);
                    }

                    Leg_LeftIndex = id;
                    if (Leg_LeftIndex > LegLeftList.Count - 1)
                    {
                        legleftint = LegLeftList.Count - 1;
                        Leg_LeftIndex = legleftint;
                    }

                    currentLeg_Left = LegLeftList[Leg_LeftIndex];
                    currentLeg_Left.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedLeg_LeftIndex = id;
                    break;
                }

            case Options.Head_Attachment:
                {
                    if (currentHead_Attachment != null)
                    {
                        currentHead_Attachment.SetActive(false);
                    }

                    Head_AttachmentIndex = id;
                    if (Head_AttachmentIndex > HeadAttachmentList.Count - 1)
                    {
                        headattachmentint = HeadAttachmentList.Count - 1;
                        Head_AttachmentIndex = headattachmentint;
                    }

                    currentHead_Attachment = HeadAttachmentList[Head_AttachmentIndex];
                    currentHead_Attachment.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHead_AttachmentIndex = id;
                    break;
                }

            case Options.Back_Attachment:
                {
                    if (currentBack_Attachment != null)
                    {
                        currentBack_Attachment.SetActive(false);
                    }

                    Back_AttachmentIndex = id;
                    if (Back_AttachmentIndex > BackAttachmentList.Count - 1)
                    {
                        backattachmentint = BackAttachmentList.Count - 1;
                        Back_AttachmentIndex = backattachmentint;
                    }

                    currentBack_Attachment = BackAttachmentList[Back_AttachmentIndex];
                    currentBack_Attachment.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedBack_AttachmentIndex = id;
                    break;
                }

            case Options.Hip_Attachment:
                {
                    if (currentHip_Attachment != null)
                    {
                        currentHip_Attachment.SetActive(false);
                    }

                    Hip_AttachmentIndex = id;
                    if (Hip_AttachmentIndex > HipAttachmentList.Count - 1)
                    {
                        hipattachmentint = HipAttachmentList.Count - 1;
                        Hip_AttachmentIndex = hipattachmentint;
                    }

                    currentHip_Attachment = HipAttachmentList[Hip_AttachmentIndex];
                    currentHip_Attachment.SetActive(true);

                    hierarchySelection.GetComponent<NpcCreatorSelector>().SavedHip_AttachmentIndex = id;
                    break;
                }
        }
    }
}
