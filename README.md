# Emmola.Helper
A c# helper function collection, most are extensions. includes type finder/helper, string/number/datetime/object helper/extensions

## Emmola.Helpers ##

# T:Emmola.Helpers.Classes.Ordered`1

 To hold object with Order index 



---
##### M:Emmola.Helpers.Classes.Ordered`1.CompareTo(Emmola.Helpers.Classes.Ordered{`0})

 compare 

|Name | Description |
|-----|------|
|other: ||
Returns: 



---
##### P:Emmola.Helpers.Classes.Ordered`1.Value

 Object to hold 



---
##### P:Emmola.Helpers.Classes.Ordered`1.Order

 Order index 



---
##### M:Emmola.Helpers.DateTimeHelper.ToDateString(System.DateTime)

 Return a Date string, set DateTimeHelper.DATE_FORMAT to change output format 



---
##### M:Emmola.Helpers.DateTimeHelper.ToTimeString(System.DateTime)

 Return a Date string, set DateTimeHelper.DATE_FORMAT to change output format 



---
##### M:Emmola.Helpers.DateTimeHelper.ToDateTimeString(System.DateTime)

 Return a Date And Time string, set DateTimeHelper.DATETIME_FORMAT to change output format 



---
##### M:Emmola.Helpers.DateTimeHelper.ToDateString(System.Nullable{System.DateTime})

 Return a Date string, set DateTimeHelper.DATE_FORMAT to change output format 



---
##### M:Emmola.Helpers.DateTimeHelper.ToTimeString(System.Nullable{System.DateTime})

 Return a Date string, set DateTimeHelper.DATE_FORMAT to change output format 



---
##### M:Emmola.Helpers.DateTimeHelper.ToDateTimeString(System.Nullable{System.DateTime})

 Return a Date And Time string, set DateTimeHelper.DATETIME_FORMAT to change output format 



---
##### M:Emmola.Helpers.DateTimeHelper.ToReadable(System.Nullable{System.DateTime},System.Boolean)

 Return a readable timespan to DateTime.Now if not null. 

|Name | Description |
|-----|------|
|self: ||
Returns: 



---
##### M:Emmola.Helpers.DateTimeHelper.ToReadable(System.TimeSpan)

 Return a readable string, positive as before, negitive as after. 

|Name | Description |
|-----|------|
|self: ||


---
##### M:Emmola.Helpers.DateTimeHelper.ReadableSpanTo(System.Nullable{System.DateTime},System.DateTime,System.Boolean)

 Return readable TimeSpan, or DateTime format if over a month. 

|Name | Description |
|-----|------|
|compare: |DateTime to be compared|
|Name | Description |
|-----|------|
|dateOnly: |Show short date format while span over a month|


---
##### M:Emmola.Helpers.DateTimeHelper.ToFileName(System.DateTime)

 Return a formatted string suitable as file name 

|Name | Description |
|-----|------|
|datetime: ||
Returns: string like 2015-05-29-102733-485



---
##### M:Emmola.Helpers.DateTimeHelper.Min(System.DateTime,System.DateTime)

 Return earlier DateTime of 2 DateTimes 

|Name | Description |
|-----|------|
|date1: ||
|Name | Description |
|-----|------|
|date2: ||
Returns: earlier



---
##### M:Emmola.Helpers.DateTimeHelper.Max(System.DateTime,System.DateTime)

 Return later DateTime of 2 DateTimes 

|Name | Description |
|-----|------|
|date1: ||
|Name | Description |
|-----|------|
|date2: ||
Returns: later



---
##### M:Emmola.Helpers.DateTimeHelper.Max(System.Nullable{System.DateTime},System.Nullable{System.DateTime})

 Return null when all are null, return valid one if another is null 

|Name | Description |
|-----|------|
|date1: ||
|Name | Description |
|-----|------|
|date2: ||


---
##### M:Emmola.Helpers.DateTimeHelper.Min(System.Nullable{System.DateTime},System.Nullable{System.DateTime})

 Return null if anyone is null. 

|Name | Description |
|-----|------|
|date1: ||
|Name | Description |
|-----|------|
|date2: ||


---
##### M:Emmola.Helpers.DateTimeHelper.ToUnixTimeStamp(System.DateTime)

 Converts a given DateTime into a Unix timestamp 

|Name | Description |
|-----|------|
|value: |Any DateTime|
Returns: The given DateTime in Unix timestamp format



---
##### M:Emmola.Helpers.DateTimeHelper.FromUnixTimeStamp(System.Int64)

 Convert a Unix timestamp to DateTime 

|Name | Description |
|-----|------|
|value: |Unit timestamp|
Returns: DateTime



---
##### M:Emmola.Helpers.DateTimeHelper.LatterOrNow(System.Nullable{System.DateTime})

 Return itself when is valid and later than DateTime.Now, otherwise return DateTime.Now 

|Name | Description |
|-----|------|
|dateTime: ||


---
# T:Emmola.Helpers.i18n.Res

 A strongly-typed resource class, for looking up localized strings, etc. 



---
##### P:Emmola.Helpers.i18n.Res.ResourceManager

 Returns the cached ResourceManager instance used by this class. 



---
##### P:Emmola.Helpers.i18n.Res.Culture

 Overrides the current thread's CurrentUICulture property for all resource lookups using this strongly typed resource class. 



---
##### P:Emmola.Helpers.i18n.Res.DAY_AFTER_TOMMORW

 Looks up a localized string similar to Day after tommorow. 



---
##### P:Emmola.Helpers.i18n.Res.DAY_BEFORE_YESTERDAY

 Looks up a localized string similar to Day before yesterday. 



---
##### P:Emmola.Helpers.i18n.Res.DAYS_AFTER

 Looks up a localized string similar to After {0} days. 



---
##### P:Emmola.Helpers.i18n.Res.DAYS_AGO

 Looks up a localized string similar to {0} days ago. 



---
##### P:Emmola.Helpers.i18n.Res.False

 Looks up a localized string similar to No. 



---
##### P:Emmola.Helpers.i18n.Res.HOURS_AFTER

 Looks up a localized string similar to After {0} hours. 



---
##### P:Emmola.Helpers.i18n.Res.HOURS_AGO

 Looks up a localized string similar to {0} hours ago. 



---
##### P:Emmola.Helpers.i18n.Res.JUST_NOW

 Looks up a localized string similar to Just now. 



---
##### P:Emmola.Helpers.i18n.Res.MINUTES_AFTER

 Looks up a localized string similar to After {0} minutes. 



---
##### P:Emmola.Helpers.i18n.Res.MINUTES_AGO

 Looks up a localized string similar to {0} minutes ago. 



---
##### P:Emmola.Helpers.i18n.Res.RIGHT_NOW

 Looks up a localized string similar to Right now. 



---
##### P:Emmola.Helpers.i18n.Res.TOMMOROW

 Looks up a localized string similar to Tommorow. 



---
##### P:Emmola.Helpers.i18n.Res.True

 Looks up a localized string similar to Yes. 



---
##### P:Emmola.Helpers.i18n.Res.YESTERDAY

 Looks up a localized string similar to Yesterday. 



---
# T:Emmola.Helpers.Interfaces.ISimilarity`1

 Able to calcuate similarity to ther 



---
##### M:Emmola.Helpers.NumberHelper.ToReadable(System.Nullable{System.Decimal},System.String)

 Return human friendly format 



---
##### M:Emmola.Helpers.NumberHelper.ToReadable(System.Decimal)

 Return human friendly format 



---
##### M:Emmola.Helpers.NumberHelper.ToCapacity(System.Int64)

 Return a capacity readable string 



---
##### M:Emmola.Helpers.NumberHelper.ToMoney(System.Decimal)

 Return a Money format 



---
##### M:Emmola.Helpers.NumberHelper.ToMoney(System.Nullable{System.Decimal})

 Return a Money format 



---
##### M:Emmola.Helpers.NumberHelper.ToReadable(System.Nullable{System.Int32})

 Return readable format with comma 



---
##### M:Emmola.Helpers.NumberHelper.ToPercentage(System.Single)

 Return percentage format. 



---
##### M:Emmola.Helpers.NumberHelper.ToReadable(System.Nullable{System.Boolean})

 Return Yes/No if not null. 

|Name | Description |
|-----|------|
|self: ||
Returns: 



---
##### M:Emmola.Helpers.NumberHelper.CalculatePages(System.Int64,System.Int64)

 Calculate pages base on pagesize 

|Name | Description |
|-----|------|
|pageSize: ||


---
##### M:Emmola.Helpers.WebHelper.GetHttpClient(System.String,System.String)

 Return a HttpClient with Host/Headers setup. 

|Name | Description |
|-----|------|
|host: |Host|
|Name | Description |
|-----|------|
|format: |Format for both ends|
Returns: HttpClient



---
##### M:Emmola.Helpers.WebHelper.GetJsonHttpClient(System.String)

 Return a Json HttpClient 



---
##### M:Emmola.Helpers.WebHelper.GetXmlHttpClient(System.String)

 Return a Xml HttpClient 



---
##### M:Emmola.Helpers.WebHelper.ReadAsHtmlAsync(System.Net.Http.HttpContent)

 Return string base on CHARSET in META tag or WebResponse.ContentType 



---
##### M:Emmola.Helpers.CollectionHelper.Fill``1(``0[],``0,System.Int32,System.Int32)

 Fill up array with specified value。 new int[4].Fill(1, 0, 3) => [1, 2, 3, ?]; 

|Name | Description |
|-----|------|
|value: |Value to fill|
|Name | Description |
|-----|------|
|start: |Fill starts from this(included)|
|Name | Description |
|-----|------|
|end: |Fill ends before this(excluded)|


---
##### M:Emmola.Helpers.CollectionHelper.Implode``1(System.Collections.Generic.IEnumerable{``0},System.String)

 Shortcut to String.Join 

|Name | Description |
|-----|------|
|separator: ||


---
##### M:Emmola.Helpers.CollectionHelper.AddToValue``3(System.Collections.Generic.IDictionary{``0,``1},``0,``2)

 Add value to pair.Value 

|Name | Description |
|-----|------|
|key: |Pair.Key|
|Name | Description |
|-----|------|
|value: |Add to Pair.Value|
Returns: 



---
##### M:Emmola.Helpers.CollectionHelper.FindOrAdd``1(System.Collections.Generic.ICollection{``0},System.Func{``0,System.Boolean},System.Func{``0})

 FirstOrDefault if not found, then create one add to collection then return it; 

|Name | Description |
|-----|------|
|find: |FirstOrDefault arg|
|Name | Description |
|-----|------|
|create: |Create a new one|
Returns: 



---
##### M:Emmola.Helpers.CollectionHelper.ToQueryString(System.Collections.Specialized.NameValueCollection)

 Return a urlencoded QueryString, support duplicated key 



---
##### M:Emmola.Helpers.CollectionHelper.GetNullabe``1(System.Collections.Specialized.NameValueCollection,System.String)

 Get Nullable 

|Name | Description |
|-----|------|
|key: ||


---
##### M:Emmola.Helpers.CollectionHelper.ToQueryString(System.Collections.Generic.IDictionary{System.String,System.String})

 Return a urlencoded QueryString 



---
##### M:Emmola.Helpers.CollectionHelper.AnySame``1(System.Collections.Generic.IEnumerable{``0})

 Check if any duplicated element in collection 



---
##### M:Emmola.Helpers.CollectionHelper.SimilarityTo``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``0})

 Calculate similarity of two Collection 

Returns: float type similarity between 0 and 1



---
##### M:Emmola.Helpers.CollectionHelper.FindMostSimilar``1(System.Collections.Generic.IEnumerable{``0},``0,System.Single)

 Find the most similar to target instance 

|Name | Description |
|-----|------|
|target: |Similar to|
|Name | Description |
|-----|------|
|baseline: |Only element's similarity above baseline are examinated|
Returns: Most similar instance in list, return null if all belong baseline



---
##### M:Emmola.Helpers.EnumHelper.GetMemberInfo(System.Enum)

 Return its MemberInfo 

|Name | Description |
|-----|------|
|self: ||
Returns: 



---
##### M:Emmola.Helpers.EnumHelper.GetDescription(System.Enum)

 Return its Description from DisplayAttribute instance 



---
##### M:Emmola.Helpers.EnumHelper.ToReadable(System.Enum)

 Return its DisplayAttribute.Name or DisplayNameAttribute.DisplayName 



---
##### M:Emmola.Helpers.ObjectHelper.Dump``1(``0,System.Int32)

 Return a shallow dump format with type name 



---
##### M:Emmola.Helpers.ObjectHelper.Get(System.Object,System.String)

 Return inst's property value; 



---
##### M:Emmola.Helpers.ObjectHelper.Set(System.Object,System.String,System.Object)

 Set inst's property value; 



---
##### M:Emmola.Helpers.ObjectHelper.GetString(System.Object,System.String)

 Return inst's property value and Convert.ToString 



---
##### M:Emmola.Helpers.ObjectHelper.ToReadable(System.Object,System.String)

 Convert property value of inst to readable format DateTime : return DATE/TIME format when DataTypeAttribute serve Decimal: return ToMoney when DataTypeAttribute.DataType == DataType.Currency IEnumerable: return string.JOIN(", ", value); 

|Name | Description |
|-----|------|
|inst: |Instance|
|Name | Description |
|-----|------|
|name: |Property Name|


---
##### M:Emmola.Helpers.ObjectHelper.ToNullable``1(System.Object)

 Similar to JavaScript logic, 0, "", null, "False" are threated false 



---
##### M:Emmola.Helpers.ObjectHelper.ToQueryString(System.Object,System.Boolean)

 Convert anomynous object to QueryString format 

|Name | Description |
|-----|------|
|inst: ||
Returns: 



---
##### M:Emmola.Helpers.ObjectHelper.ToBinary(System.Object)

 Convert an object to byte[] 

|Name | Description |
|-----|------|
|inst: |object to be converted|
Returns: byte[]



---
##### M:Emmola.Helpers.StringHelper.DigestMD5(System.String)

 Return its MD5 digest text 



---
##### M:Emmola.Helpers.StringHelper.DigestSHA1(System.String)

 Return its SHA1 digest text 



---
##### M:Emmola.Helpers.StringHelper.RandomText(System.Int32,System.Byte,System.Byte)

 Generate specified legnth of random characters 

|Name | Description |
|-----|------|
|length: |Length|
|Name | Description |
|-----|------|
|start: |Start|
|Name | Description |
|-----|------|
|end: |End|


---
##### M:Emmola.Helpers.StringHelper.RandomDigits(System.Int32)

 Generate specified legnth of Digits [0-9] 

|Name | Description |
|-----|------|
|length: |Length|


---
##### M:Emmola.Helpers.StringHelper.RandomLetters(System.Int32)

 Generate specified legnth of Letters [A-Za-z] 

|Name | Description |
|-----|------|
|length: |Length|


---
##### M:Emmola.Helpers.StringHelper.RandomCaptcha(System.Int32)

 Generate specifed captcha code [A-Z0-9] 

|Name | Description |
|-----|------|
|length: |Length|


---
##### M:Emmola.Helpers.StringHelper.ToHexString(System.Byte[])

 Convert Hashed byte[] to Hex String 

|Name | Description |
|-----|------|
|bytes: |Hashed byte[]|


---
##### M:Emmola.Helpers.StringHelper.IsEmpty(System.String)

 Check if equals to String.Empty 



---
##### M:Emmola.Helpers.StringHelper.IsNull(System.String)

 Check if is null 



---
##### M:Emmola.Helpers.StringHelper.IsValid(System.String)

 Check if is valid string, not null not empty. 



---
##### M:Emmola.Helpers.StringHelper.Repeat(System.String,System.Int32)

 Return itself repeated in specified times: "a".Repeat(3) = "aaa"; 

|Name | Description |
|-----|------|
|times: |How many times to repeat|


---
##### F:Emmola.Helpers.StringHelper._textInfo

 Return Title Case Format "foo bar".ToTitleCase() = "Foo Bar" 



---
##### M:Emmola.Helpers.StringHelper.Ellipsis(System.String,System.Int32)

 Return a truncated string ends with "´" when its length exceeds specified length Otherwise return itself only. 

|Name | Description |
|-----|------|
|length: |Max Length|


---
##### M:Emmola.Helpers.StringHelper.OrDefault(System.String,System.String,System.Object[])

 Return defaultValue while itself is not valid 

|Name | Description |
|-----|------|
|defaultValue: |潮範峙|
Returns: 



---
##### M:Emmola.Helpers.StringHelper.Mask(System.String,System.Char)

 Return a masked string, useful for Email/Phone masking 

|Name | Description |
|-----|------|
|mask: |Mask character|


---
##### M:Emmola.Helpers.StringHelper.IsDigitOnly(System.String)

 Check if it's valid and consist by digits only 



---
##### M:Emmola.Helpers.StringHelper.AppendTabbedLine(System.Text.StringBuilder,System.Int32,System.String,System.Object[])

 Append a newline and insert specified length of tabs in the beginning, then append text and args as String.Format 

|Name | Description |
|-----|------|
|tabs: |How many tabs to be inserted|
|Name | Description |
|-----|------|
|text: |Text to append|
|Name | Description |
|-----|------|
|args: |Supply args when text is FORMAT|


---
##### M:Emmola.Helpers.StringHelper.AppendSpaced(System.Text.StringBuilder,System.Int32,System.String,System.Object[])

 Append specified length of space before text. 

|Name | Description |
|-----|------|
|length: |Length of Spaces|
|Name | Description |
|-----|------|
|text: |Text to append|
|Name | Description |
|-----|------|
|args: |Format args|


---
##### M:Emmola.Helpers.StringHelper.AppendSpaced(System.Text.StringBuilder,System.String,System.Object[])

 Shortcut to .AppendSpaced(1, text, args); 



---
##### M:Emmola.Helpers.StringHelper.AllValid(System.String[])

 Return all valid string as a new string[] 



---
##### M:Emmola.Helpers.StringHelper.IcEquals(System.String,System.String)

 Shortcut of String.Equals InvariantCultureIgnoreCase 



---
##### M:Emmola.Helpers.StringHelper.FormatMe(System.String,System.Object[])

 Shortcut of String.Format 



---
##### M:Emmola.Helpers.StringHelper.ToHtmlString(System.String)

 Return as Html Format 



---
##### M:Emmola.Helpers.TypeHelper.IsSimpleType(System.Type)

 Check if type is simple type, such as value type, string, enum, decimal。 



---
##### M:Emmola.Helpers.TypeHelper.FindSubTypes(System.Type,System.Collections.Generic.IEnumerable{System.Reflection.Assembly},System.Boolean,System.String)

 Return all SubTypes in specified Assemblies 

|Name | Description |
|-----|------|
|assemblies: |Assemblies to search|
|Name | Description |
|-----|------|
|concreateOnly: |Concreate type only|
|Name | Description |
|-----|------|
|nameSpace: |Search with in specifed namespace|


---
##### M:Emmola.Helpers.TypeHelper.FindSubTypes(System.Type,System.Boolean,System.String)

 Return all SubTypes in current AppDomain 

|Name | Description |
|-----|------|
|concreateOnly: |Concreate type only|
|Name | Description |
|-----|------|
|nameSpace: |Search with in specifed namespace|


---
##### M:Emmola.Helpers.TypeHelper.FindSubTypesInMe(System.Type,System.Boolean,System.String)

 Return all SubTypes in calling Assembly 

|Name | Description |
|-----|------|
|concreateOnly: |Concreate type only|
|Name | Description |
|-----|------|
|nameSpace: |Search with in specifed namespace|


---
##### M:Emmola.Helpers.TypeHelper.IsEnumerable(System.Type)

 Check if is IEnumerable, including Array, GenericIEnumerable but NOT string! 



---
##### M:Emmola.Helpers.TypeHelper.GetNullableValueType(System.Type)

 Return Nullable value type. 



---
##### M:Emmola.Helpers.TypeHelper.HasAttribute(System.Reflection.MemberInfo,System.Type,System.Boolean)

 Check if contains a specified Attribute 

|Name | Description |
|-----|------|
|attributeType: |Type of Attribute to find|


---
##### M:Emmola.Helpers.TypeHelper.HasAttribute``1(System.Reflection.MemberInfo,System.Boolean)

 HasAttribute Generic Version 



---
##### M:Emmola.Helpers.TypeHelper.GetAttribute``1(System.Reflection.MemberInfo,System.Boolean)

 Return specified Attribute Type instance 



---
##### M:Emmola.Helpers.TypeHelper.IfAttribute``1(System.Reflection.MemberInfo,System.Action{``0},System.Boolean)

 Run action if Attribute exists 



---
##### M:Emmola.Helpers.TypeHelper.GetPublicProperties(System.Type)

 Return all public Properties 



---
##### M:Emmola.Helpers.TypeHelper.GetReadWriteProperties(System.Type)

 Return all public Properties 



---
##### M:Emmola.Helpers.TypeHelper.GetDisplayAttr(System.Reflection.MemberInfo)

 Return its DisplayAttribute instance; 



---
##### M:Emmola.Helpers.TypeHelper.GetDisplayNameAttr(System.Reflection.MemberInfo)

 Return its DisplayNameAttribute instance; 



---
##### M:Emmola.Helpers.TypeHelper.GetName(System.Reflection.MemberInfo,System.String)

 Return DisplayAttribute.Name or DisplayNameAttribute.DisplayName 

|Name | Description |
|-----|------|
|defaultName: |In case none of them is found, and will fallback to TypeName if no defaultName given|
Returns: 



---
##### M:Emmola.Helpers.TypeHelper.GetDescription(System.Reflection.MemberInfo)

 Return its Description from DisplayAttribute instance, return null when none is defined 



---



