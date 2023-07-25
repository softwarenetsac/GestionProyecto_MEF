using System;
using System.Reflection;

namespace Gestion_Rendimiento_Laserfiche.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}