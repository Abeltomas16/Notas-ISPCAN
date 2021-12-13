# Notas-ISPCAN
Aplicativo mobile, android, para gestão das notas dos estudantes
Aplicativo que vai ajudar os estudantes, a receberem as suas notas em qualquer lugar, bem como ajudar os docentes a lancarem as notas a qualquer hora. 

#### Tecnologias
* [Firebase](https://https://console.firebase.google.com/)
* [Xamarin Forms](https://github.com/xamarin/Xamarin.Forms)

## Uso
#### Actualizar pacotes

```shell
Update-Package -Reinstall
```
#### Configurar locator

```shell
   Locator.CurrentMutable.Register(() => new FirebaseAuthProvider(new FirebaseConfig("API KEY FIREBASE")));
   Locator.CurrentMutable.Register(() => new FirebaseClient("URL PROJECTO FIREBASE"));
```

## Tests

```shell
Install-Package NUnit -Version 3.13.2
```

## License

[The MIT License](http://opensource.org/licenses/MIT)

Copyright (c) 2011-2013 Jared Hanson <[http://jaredhanson.net/](http://jaredhanson.net/)>
