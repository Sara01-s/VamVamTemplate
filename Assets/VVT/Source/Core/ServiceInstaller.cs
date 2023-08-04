using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VVT;

internal sealed class ServiceInstaller : MonoBehaviour {

    private readonly List<ServiceAttribute> _serviceAttributes = new();

    [Service] private float _testField;


    [Service] private int TestProperty { get; set; } = 0;

    private void Awake() {
        print("Test property before : " + TestProperty);
        InjectDependencies();
        print("Test property after : " + TestProperty);
    }

    private void InjectDependencies() {

        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                                .Where(asm => asm.FullName.StartsWith("VVT"));

        foreach (var assembly in assemblies) {
            //print(assembly.FullName.CutAfter(','));

            IEnumerable<Type> serviceTypes = assembly.GetTypes()
                                                     .Where(type => type.IsClass || type.IsAbstract);

            // foreach (var s in serviceTypes) print(s);
            if (!serviceTypes.Any()) {
                //print("DI Container : No injectables found in assembly: " + assembly.FullName.CutAfter(','));
                continue;
            }

            foreach (var type in serviceTypes) {
                var typesFilter = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                var serviceMembers = type.GetMembers(typesFilter)
                                         .Where(member => 
                                            member.MemberType is MemberTypes.Field or MemberTypes.Property
                                            && member.GetCustomAttribute<ServiceAttribute>() is not null
                                         );

                foreach (var service in serviceMembers) {

                    if (service is FieldInfo field) {

                    }
                    else if (service is PropertyInfo property) {
                        property.SetValue(this, 10);
                    }
                    else {
                        Debug.LogError("DI Container : retrieved service is not a Field or a Property");
                    }

                }

            }
        }

    }

}
