using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.UI {
    public interface ITalk {
        Coroutine ShowDialogue(string content);

        Coroutine HideDialogue();
    }
}
