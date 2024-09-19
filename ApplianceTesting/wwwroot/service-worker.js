self.addEventListener('install', function (event) {
    event.waitUntil(
        caches.open('my-cache').then(function (cache) {
            const urlsToCache = [
                '/',
                '/Home/Login',
                '/Master/Index',
                //'/Master/State',
                //'/Master/City',
                //'/Master/Location',
                '/Master/StateCityLocation',
                '/Master/SignOut',
                '/css/site.css',
                '/css/StyleSheet.css',
                '/js/site.js',
                '/js/JavaScript.js',
                '/js/jquery.min.js',
                '/js/sweetalert.min.js',
                '/icon/icon192x192.png',
                '/icon/icon512x512.png',
                '/usrCred.json'

            ];

            console.log('Caching resources:', urlsToCache);

            return cache.addAll(urlsToCache).catch(function (error) {
                console.error('Failed to cache resources:', error);
            });
        })
    );
});

self.addEventListener('fetch', function (event) {
    if (event.request.method === 'GET') {
        event.respondWith(
            caches.match(event.request).then(function (response) {
                return response || fetch(event.request).catch(function () {
                    return caches.match('/offline.html');  // Provide an offline fallback
                });
            }).catch(function (error) {
                console.error('Fetch error:', error);
                return new Response('Network error or offline', {
                    status: 503,
                    headers: { 'Content-Type': 'text/plain' }
                });
            })
        );
    } else {
        event.respondWith(
            fetch(event.request).catch(function () {
                return new Response(JSON.stringify({ error: 'Network error or offline' }), {
                    status: 503,
                    headers: { 'Content-Type': 'application/json' }
                });
            })
        );
    }
});
