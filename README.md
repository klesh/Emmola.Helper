# Introduction
.NET Helper functions collection for reducing reinventing and better readable code


## TypeHelper

### Type.IsSimpleType()
Check if a Type is a simple type (ValueType/String/Decimal)

```
Assert.IsTrue(typeof(string).IsSimpleType());
Assert.IsTrue(typeof(int).IsSimpleType());
Assert.IsTrue(typeof(Status).IsSimpleType());
Assert.IsTrue(typeof(decimal).IsSimpleType());
Assert.IsFalse(typeof(Foo).IsSimpleType());
```