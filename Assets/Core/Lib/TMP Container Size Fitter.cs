/*****************************************************
// File: TMP Container Size Fitter
// Author: The Messy Coder
// Date: March 2022
// Please support by following on Twitch & social media
// Twitch:   www.Twitch.tv/TheMessyCoder
// YouTube:  www.YouTube.com/TheMessyCoder
// Facebook: www.Facebook.com/TheMessyCoder
// Twitter:  www.Twitter.com/TheMessyCoder
*****************************************************/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Messy
{
    [ExecuteAlways, AddComponentMenu("Layout/TMP Container Size Fitter")]
    public class TextContainerFitter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_TMProUGUI;

        private TextMeshProUGUI TextMeshPro
        {
            get
            {
                if (m_TMProUGUI == null && transform.GetComponentInChildren<TextMeshProUGUI>())
                {
                    m_TMProUGUI = transform.GetComponentInChildren<TextMeshProUGUI>();
                    m_TMPRectTransform = m_TMProUGUI.rectTransform;
                }

                return m_TMProUGUI;
            }
        }

        private RectTransform m_RectTransform;

        private RectTransform rectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = GetComponent<RectTransform>();
                }

                return m_RectTransform;
            }
        }

        private RectTransform m_TMPRectTransform;

        public RectTransform TMPRectTransform
        {
            get { return m_TMPRectTransform; }
        }

        private float m_PreferredHeight;

        private float PreferredHeight
        {
            get { return m_PreferredHeight; }
        }

        protected virtual void SetHeight()
        {
            if (TextMeshPro == null)
                return;

            m_PreferredHeight = TextMeshPro.preferredHeight;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, PreferredHeight);

            SetDirty();
        }

        protected virtual void OnEnable()
        {
            SetHeight();
        }

        protected virtual void Start()
        {
            SetHeight();
        }

        protected virtual void Update()
        {
            if (PreferredHeight != TextMeshPro.preferredHeight)
            {
                SetHeight();
            }
        }

        #region MarkLayoutForRebuild

        protected virtual bool IsActive()
        {
            return isActiveAndEnabled;
        }

        private void SetDirty()
        {
            if (!IsActive())
                return;

            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        }


        protected virtual void OnRectTransformDimensionsChange()
        {
            SetDirty();
        }


#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            SetDirty();
        }
#endif

        #endregion
    }
}