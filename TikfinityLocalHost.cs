using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class TikfinityLocalHost : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    private HttpListener listener;
    public int port = 3002;
    private Queue<Action> actions = new Queue<Action>();

    // Duplicate request filtresi için
    private string lastUrl = "";
    private float lastTime = 0f;
    private readonly float duplicateThreshold = 0.05f; // saniye

    private async void Start()
    {
        infoText.text = "Event'ler bekleniyor...";

        listener = new HttpListener();
        string prefix = $"http://localhost:{port}/";
        listener.Prefixes.Add(prefix);

        try
        {
            listener.Start();
            Debug.Log($"Server dinleniyor: {prefix}");
        }
        catch (Exception ex)
        {
            Debug.LogError("HttpListener başlatılamadı!");
            Debug.LogException(ex);
            return;
        }

        // Listener’ı arka planda çalıştır
        _ = Task.Run(async () =>
        {
            while (listener.IsListening)
            {
                try
                {
                    var context = await listener.GetContextAsync();
                    lock (actions)
                    {
                        actions.Enqueue(() => HandleRequest(context));
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("HttpListener hata: " + e.Message);
                }
            }
        });
    }

    void Update()
    {
        lock (actions)
        {
            while (actions.Count > 0)
            {
                actions.Dequeue()?.Invoke();
            }
        }
    }

    private void HandleRequest(HttpListenerContext ctx)
    {
        var req = ctx.Request;
        var res = ctx.Response;

        string path = req.Url.AbsolutePath.Trim('/').ToLower();

        // favicon otomatik isteğini engelle
        if (path == "favicon.ico")
        {
            res.StatusCode = 404;
            res.Close();
            return;
        }

        // Duplicate filtre kontrolü
        if (req.RawUrl == lastUrl && Time.time - lastTime < duplicateThreshold)
        {
            Debug.LogWarning("Aynı request kısa sürede tekrar geldi, yok sayıldı: " + req.RawUrl);
            WriteResponse(res, "Duplicate request ignored", 200);
            return;
        }

        lastUrl = req.RawUrl;
        lastTime = Time.time;

        string username = req.QueryString["username"]; // query parametre

        if (path.StartsWith("event"))
        {
            if (int.TryParse(path.Substring(5), out int n) && n >= 1 && n <= 19)
            {
                TriggerEvent(n, username);
                WriteResponse(res, $"OK - tetiklendi: event{n}, user={username}");
                Debug.Log($"Siteye gidildi: {req.Url} -> event{n} tetiklendi, user={username}");
                return;
            }
        }

        Debug.LogWarning($"Bilinmeyen endpoint: {req.Url}");
        WriteResponse(res, "Unknown endpoint", 404);
    }

    private void WriteResponse(HttpListenerResponse res, string text, int statusCode = 200)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text);
        res.StatusCode = statusCode;
        res.ContentLength64 = buffer.Length;
        res.ContentType = "text/plain; charset=utf-8";
        var output = res.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        res.Close();
    }

    private void TriggerEvent(int n, string username)
    {
        Debug.Log($"TriggerEvent çağrıldı -> event{n}, user={username}"); // Kontrol logu

        switch (n)
        {
            case 1: Event1(); break;
            case 2: Event2(); break;
            case 3: Event3(); break;
            case 4: Event4(); break;
            case 5: Event5(); break;
        }
    }

    private void Event1()
    {
        infoText.text = "Event1 tetiklendi!";
    }
    private void Event2()
    {
        infoText.text = "Event2 tetiklendi!";
    }
    private void Event3()
    {
        infoText.text = "Event3 tetiklendi!";
    }
    private void Event4()
    {
        infoText.text = "Event4 tetiklendi!";
    }
    private void Event5()
    {
        infoText.text = "Event5 tetiklendi!";
    }
    //......

    private void OnDisable() => CloseListener();
    private void OnApplicationQuit() => CloseListener();

    private void CloseListener()
    {
        if (listener != null && listener.IsListening)
        {
            listener.Stop();
            listener.Close();
            listener = null;
            Debug.Log("HttpListener kapatıldı.");
        }
    }
}
