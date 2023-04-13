using System;
using UnityEngine;

namespace Shop.Client
{
    public class AIIntentionWidget : MonoBehaviour
    {
        [SerializeField] private ClientAIController _clientAI;
        [SerializeField] private IntentionWidget[] _intentionWidgets;

        private void OnEnable()
        {
            _clientAI.OnIntentionChanged += UpdateIntention;
        }

        private void UpdateIntention(IntentionType intentionType)
        {
            foreach (var widget in _intentionWidgets)
            {
                if (widget.IntentionType == intentionType)
                {
                    widget.Widget.SetActive(true);
                    continue;
                }

                widget.Widget.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _clientAI.OnIntentionChanged -= UpdateIntention;
        }
    }

    [Serializable]
    public class IntentionWidget
    {
        [SerializeField] private IntentionType _intentionType;
        [SerializeField] private GameObject _widget;

        public IntentionType IntentionType => _intentionType;

        public GameObject Widget => _widget;
    }
}