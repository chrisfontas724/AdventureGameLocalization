using System.Collections.Generic;
using UnityEngine;

// The view manager is responsible for managing UI views
// on the main canvas.
public class ViewManager : MonoBehaviour
{
    // Singleton class.
    public static ViewManager Instance { get; private set; }

    // All of the views that are registered with the ViewManager.
    [SerializeField] private View[] _views;

    // Determines if first view should be shown by default or not.
    [SerializeField] private bool _show_by_default;

    // The view that is currently active and being displayed on
    // the main canvas.
    private View _current_view;

    // The current stack of views that have led to the _current_view
    // being enabled.
    private Stack<View> history_ = new Stack<View>();

    // Initialize the singleton, make sure that there is only one
    // instance in use.
    private void Awake()
    {
        Debug.Log("Create view manager instance!\n");
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    // Grab a reference to the view of type T registered with the ViewManager.
    // If that type does not exist, this function returns null.
    public T GetView<T>() where T : View
    {
        for (int i = 0; i < _views.Length; i++)
        {
            if (_views[i] is T view)
            {
                return view;
            }
        }
        return null;
    }

    // Given a view of type T, if that view is registered with the ViewManager,
    // then set that view as the current view and set it to be active. If
    // |remember| = true, then push the previous _current_view onto the stack so
    // when the new view is removed, the previous _current_view resumes its place.
    public void Show<T>(bool remember= true) where T : View
    {
        for (int i = 0; i < _views.Length; i++) 
        {
            if (_views[i] is T) 
            {
                if (_current_view != null) 
                {
                    if (remember) 
                    {
                        history_.Push(_current_view);
                    }
                    _current_view.Hide();
                }

                _views[i].Show();
                _current_view = _views[i];
            }
        }
    }

    // Alternate version of |Show| where instead of a template, the caller passes
    // in a reference to a view directly. This also allows the caller to show views
    // that might not have already been preregistered with the manager.
	public void Show(View view, bool remember = true)
    {
        if (_current_view != null)
        {
            if (remember)
            {
                history_.Push(_current_view);
            }

           _current_view.Hide();
        }

        view.Show();
        _current_view = view;
    }

    // Shows a new view on top of the current view without hiding the current view.
    // This can be useful, for instance, when pulling up an inventory item in real
    // time over the HUD, while the game continues without pausing.
    public void ShowOverlay<T>() where T : View
    {
        for (int i = 0; i < _views.Length; i++)
        {
            if (_views[i] is T)
            {
                if (_current_view != null)
                {
                        history_.Push(_current_view);
                }

                _views[i].Show();
                _current_view = _views[i];
            }
        }
    }

    // Removes the current view and shows the previous one. If there were no previous views,
    // then this function does not do anything.
    public void ShowLast()
    {
        if (history_.Count != 0)
        {
            Show(history_.Pop(), /*remember*/false);
        }
    }

    // Make sure that all vies are initialized and hidden to begin with.
    private void Start()
    {
        for (int i = 0; i < _views.Length; i++)
        {
            _views[i].Initialize();

            if (i == 0 && _show_by_default)
            {
                Show(_views[i]);
                continue;
            }
            _views[i].Hide();
        }
    }
}
