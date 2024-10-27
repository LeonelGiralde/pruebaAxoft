# Academia Challenge

## Axoft



### Objetivos

  ¡Bienvenido al **AcademiaChallenge** de Axoft!
  
  El objetivo de este desafío es brindarte la oportunidad de demostrar tus conocimientos y habilidades técnicas.

  Recuerda que dispones de **48 horas** para completar el desafío.



### Uso de GIT

  Para resolver este ejercicio, realiza un **fork** del repositorio en un repositorio privado. Asegúrate de asignar el rol de **reporter** al usuario `AxoftTango`, para que podamos acceder y revisar tu trabajo.

  La evaluación se llevará a cabo sobre la rama `main` de tu fork, así que asegúrate de que todo el código esté actualizado y funcionando correctamente en dicha rama.



### Aclaraciones

#### Solución

La solución está creada utilizando **Visual Studio**, aunque también podés usar **Visual Studio Code** si lo preferís. El proyecto se compone de dos partes:
- **AcademiaChallenge**: una *Class Library* que contiene la lógica de negocio.
- **AcademiaChallengeTests**: un proyecto de tests que permite verificar el correcto funcionamiento de la lógica de negocio.

#### Lenguaje de programación

El lenguaje principal del desafío es **C#**. No obstante, podés entregar la solución en otro lenguaje de programación con el que te sientas más cómodo, siempre que respetes los nombres, las firmas de los métodos, y las estructuras de datos especificadas.

#### Buenas prácticas en código

Se valorará positivamente el uso de **buenas prácticas** de programación, como la correcta **modularización**, nombres **descriptivos**, el manejo de **warnings**, y la aplicación de sugerencias (**hints**). 

#### Integración continua

Se provee un pipeline de **integración continua**. Cada vez que realices un *push* a la rama `main`, este pipeline se ejecutará automáticamente. Asegúrate de que el código compile correctamente y que los tests se ejecuten sin problemas antes de cada *push*.



### Validaciones

Se deben implementar las siguientes validaciones en el sistema.

La clase `ProcesadorFacturasAxoft` cuenta con un método `Validar` donde se completarán estas validaciones.

A modo de ejemplo, la primera validación ya está implementada:

  1. Validar que la numeración de las facturas comienza en 1, es correlativa y no presenta huecos.
  2. Verificar que las facturas están emitidas en orden cronológico. Por ejemplo, si la factura 1 tiene fecha del 5 de enero, la factura 2 no puede tener fecha anterior.
  3. Un mismo cliente debe tener siempre el mismo CUIL,CodigoCliente y porcentaje de IVA.
  4. Un mismo artículo debe mantener el mismo código, descripción y precio unitario en todas las facturas.
  5. El orden de numeración de los renglones dentro de cada factura debe ser correcto.
  6. El total de cada renglón debe estar calculado correctamente.
  7. El total sin IVA de cada factura debe ser correcto.
  8. El porcentaje de IVA de cada factura debe ser 0%, 10.5%, 21% o 27%.
  9. El importe del IVA de cada factura debe estar calculado correctamente.
  10. El total con IVA de cada factura debe ser correcto.



### Consultas

El sistema debe ser capaz de resolver las siguientes consultas. Para cada una de ellas, ya existe un método en la clase `ProcesadorFacturasAxoft` encargado de implementarlas. Como referencia, la primera consulta ya está implementada:

  1. **Total facturado**: Calcula el total facturado sumando los totales de todas las facturas.
  2. **Código del artículo con más unidades vendidas**: Devuelve el código del artículo que ha sido vendido en mayor cantidad.
  3. **Cliente que más gastó**: Devuelve la razón social del cliente que realizó el mayor gasto total.
  4. **Artículo más comprado por un cliente**: Devuelve la descripción del artículo mas comprado por un cliente.
  5. **Total facturado en una fecha específica**: Calcula el total facturado en la fecha dada.
  6. **Cliente que más compró un artículo**: Devuelve el CUIL del cliente que compró más cantidad de un artículo en particular.



### UnitTests 
  
  La clase de tests `ProcesadorFacturasAxoftTest` ya cuenta con algunos UnitTests implementados a modo de ejemplo.

  Estos tests aseguran que los métodos proporcionados hasta el momento funcionan de acuerdo con las especificaciones.

#### Ejemplos de tests ya implementados:
- El método Validar para una lista de facturas válidas no arroja ninguna excepción.
- El método Validar para una lista de facturas con numeración incorrecta arroja la excepción NumeracionInvalidaException con el mensaje "Numeración inválida".
- El método TotalFacturado para una lista de facturas devuelve el valor correcto.

#### Tu desafío
  Nos gustaría que completes los UnitTests para los métodos faltantes. El objetivo es que asegures la correcta implementación de las funcionalidades que programaste, por ejemplo:

  - **Validaciones:** Implementa los UnitTests necesarios para validar las condiciones y restricciones planteadas en la sección de Validaciones.
    1. ¿El sistema arroja la excepción correcta cuando las facturas no están en orden cronológico?
    2. ¿Se valida correctamente que un mismo cliente siempre tenga los mismos datos?

  - **Consultas:** Crea los UnitTests para las consultas que aún no están cubiertas, asegurando que devuelvan los valores esperados. Ejemplos de lo que deberías testear:
    1. ¿El código del artículo con más unidades vendidas es el correcto?
    2. ¿El cliente que más gastó es identificado correctamente?

Un buen conjunto de tests te ayudará a validar que la lógica del sistema está correctamente implementada y a identificar errores antes de que ocurran en producción.

¿Te animás a completar los UnitTests? ¡Te estamos esperando!
