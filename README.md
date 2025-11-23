#EN
# Tikfinity Unity Integration

This script allows you to receive events from TikTok Live gifts in your Unity game using Tikfinity. It starts a local web server that listens for requests from Tikfinity and triggers corresponding events in Unity.

## How It Works

The `TikfinityLocalHost.cs` script creates a local server at `http://localhost:3002`. When a gift is received on your TikTok Live stream, you can configure Tikfinity to send a request to this local server. The script then parses the request and triggers a specific event in your game.

For example, if Tikfinity sends a request to `http://localhost:3002/Event1`, the `Event1()` method in the script will be called.

## How to Use

1.  Add the `TikfinityLocalHost.cs` script to a GameObject in your Unity scene.
2.  Assign a `TextMeshProUGUI` component to the `infoText` field in the Inspector if you want to display event information on the screen.
3.  In Tikfinity, set up your gift events to trigger a "Fetch URL" action.
4.  Configure the URL to point to your local server, followed by the event name you want to trigger. For example: `http://localhost:3002/Event1`

## Customizing Events

To create your own custom events, you can modify the `Event1()`, `Event2()`, etc. methods within the `TikfinityLocalHost.cs` script. You can add your own game logic inside these methods to create different interactions for different gifts.

```csharp
    private void Event1()
    {
        // Your custom code for Event1 goes here
        infoText.text = "Event1 triggered!";
    }

    private void Event2()
    {
        // Your custom code for Event2 goes here
        infoText.text = "Event2 triggered!";
    }
```

---

#TR
# Tikfinity Unity Entegrasyonu

Bu script, Tikfinity kullanarak Unity oyununuzda TikTok Canlı yayın hediyelerinden gelen olayları almanızı sağlar. Tikfinity'den gelen istekleri dinleyen yerel bir web sunucusu başlatır ve Unity'de karşılık gelen olayları tetikler.

## Nasıl Çalışır?

`TikfinityLocalHost.cs` script'i `http://localhost:3002` adresinde yerel bir sunucu oluşturur. TikTok Canlı yayınınızda bir hediye alındığında, Tikfinity'yi bu yerel sunucuya bir istek gönderecek şekilde yapılandırabilirsiniz. Script daha sonra isteği ayrıştırır ve oyununuzda belirli bir olayı tetikler.

Örneğin, Tikfinity `http://localhost:3002/Event1` adresine bir istek gönderirse, script içerisindeki `Event1()` metodu çağrılır.

## Nasıl Kullanılır?

1.  `TikfinityLocalHost.cs` script'ini Unity sahnenizdeki bir GameObject'e ekleyin.
2.  Ekranda olay bilgilerini göstermek istiyorsanız, Inspector'daki `infoText` alanına bir `TextMeshProUGUI` bileşeni atayın.
3.  Tikfinity'de, hediye olaylarınızı "Fetch URL" eylemini tetikleyecek şekilde ayarlayın.
4.  URL'yi yerel sunucunuzu ve ardından tetiklemek istediğiniz olayın adını gösterecek şekilde yapılandırın. Örneğin: `http://localhost:3002/Event1`

## Olayları Özelleştirme

Kendi özel olaylarınızı oluşturmak için `TikfinityLocalHost.cs` script'i içindeki `Event1()`, `Event2()` vb. metodları düzenleyebilirsiniz. Farklı hediyeler için farklı etkileşimler oluşturmak üzere bu metodların içine kendi oyun mantığınızı ekleyebilirsiniz.

```csharp
    private void Event1()
    {
        // Event1 için kendi özel kodunuzu buraya yazın
        infoText.text = "Event1 tetiklendi!";
    }

    private void Event2()
    {
        // Event2 için kendi özel kodunuzu buraya yazın
        infoText.text = "Event2 tetiklendi!";
    }
```
