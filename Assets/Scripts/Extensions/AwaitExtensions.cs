using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityAsyncAwaitUtil;
using UnityEngine;
using static IEnumeratorAwaitExtensions;

namespace MyGameApplication {
    public static class AwaitExtensions {
        public static SimpleCoroutineAwaiter GetAwaiter(this Coroutine instruction) {
            var awaiter = new SimpleCoroutineAwaiter();
            RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                InstructionWrappers.ReturnVoid(awaiter, instruction)));
            return awaiter;
        }
        static void RunOnUnityScheduler(Action action) {
            if (SynchronizationContext.Current == SyncContextUtil.UnitySynchronizationContext) {
                action();
            }
            else {
                SyncContextUtil.UnitySynchronizationContext.Post(_ => action(), null);
            }
        }

        static class InstructionWrappers {
            public static IEnumerator ReturnVoid(
                SimpleCoroutineAwaiter awaiter, object instruction) {
                // For simple instructions we assume that they don't throw exceptions
                yield return instruction;
                awaiter.Complete(null);
            }

            public static IEnumerator AssetBundleCreateRequest(
                SimpleCoroutineAwaiter<AssetBundle> awaiter, AssetBundleCreateRequest instruction) {
                yield return instruction;
                awaiter.Complete(instruction.assetBundle, null);
            }

            public static IEnumerator ReturnSelf<T>(
                SimpleCoroutineAwaiter<T> awaiter, T instruction) {
                yield return instruction;
                awaiter.Complete(instruction, null);
            }

            public static IEnumerator AssetBundleRequest(
                SimpleCoroutineAwaiter<UnityEngine.Object> awaiter, AssetBundleRequest instruction) {
                yield return instruction;
                awaiter.Complete(instruction.asset, null);
            }

            public static IEnumerator ResourceRequest(
                SimpleCoroutineAwaiter<UnityEngine.Object> awaiter, ResourceRequest instruction) {
                yield return instruction;
                awaiter.Complete(instruction.asset, null);
            }
        }
    }
}
